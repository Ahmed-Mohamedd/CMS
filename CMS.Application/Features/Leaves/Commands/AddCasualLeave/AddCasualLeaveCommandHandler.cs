using CMS.Application.Common.CQRS;
using CMS.Application.Features.Leaves.Utilities;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace CMS.Application.Features.Leaves.Commands.AddCasualLeave
{
    class AddCasualLeaveCommandHandler(IApplicationDbContext context)
        : ICommandHandler<AddCasualLeaveCommand, int>
    {
        public async Task<int> Handle(AddCasualLeaveCommand request, CancellationToken cancellationToken)
        {
            var person = await context.Persons
                                      .Include(c => c.LeaveBalances)
                                      .FirstOrDefaultAsync(p => p.Id == request.dto.PersonId);

            var leaveType = await context.LeaveType.FirstOrDefaultAsync(x => x.Name == "عارضة");

            //check if leaveType allowed for this person
            if (await Helper.IsLeaveTypeAllowedForPerson(leaveType!.Id, person!.PersonTypeId, context))
            {
                //check for his leave balance
                var leaveBalance = person.LeaveBalances.FirstOrDefault(x => x.LeaveTypeId == leaveType.Id);
                var availableBalance = leaveBalance!.TotalDays - leaveBalance.TakenDays;

                if (availableBalance > 0)
                {
                    var leave = new Leave
                    {
                        PersonId = person.Id,
                        LeaveTypeId = leaveType.Id,
                        DepartDate = request.dto.DepartDate,
                        DepartHour = request.dto.DepartHour,
                        ReturnDate = request.dto.ReturnDate,
                        ReturnHour = request.dto.ReturnHour,
                        Notes = request.dto.Notes,
                    };
                    await context.Leave.AddAsync(leave);
                    person.IsAbsent = true;

                    leaveBalance!.TakenDays += 1;
                    
                    await context.SaveChangesAsync(cancellationToken);
                    return 1;
                }
                return 0;
            }
            return 0;
        }
    }
}
