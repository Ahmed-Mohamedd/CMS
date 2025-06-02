using CMS.Application.Common.CQRS;
using CMS.Application.Features.Leaves.Utilities;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Leaves.Commands.AddAnnualLeave
{
    public class AddAnnualLeaveCommandHandler(IApplicationDbContext context) : ICommandHandler<AddAnnualLeaveCommand, int>
    {
        public async Task<int> Handle(AddAnnualLeaveCommand request, CancellationToken cancellationToken)
        {

            var person = await context.Persons
                                       .Include(x => x.LeaveBalances)
                                       .FirstOrDefaultAsync(x => x.Id == request.dto.PersonId);

            var leaveType = await context.LeaveType.FirstOrDefaultAsync(x => x.Name == "سنوية");

            // Get the current date and the year
            var now = DateTime.Now;
            int currentYear = now.Year;

            bool isFirstHalf = now.Month <= 6; // first half
            int firstHalfMaxDays = 15;
            int secondHalfMaxDays = 15;

            //check if leaveType allowed for this person
            if (await Helper.IsLeaveTypeAllowedForPerson(leaveType!.Id, person!.PersonTypeId, context))
            {
                //check for his leave balance
                var leaveBalance = person.LeaveBalances
                                  .FirstOrDefault(x => x.LeaveTypeId == leaveType.Id);

                //calc how many days person request
                int requestDays = (request.dto.ReturnDate - request.dto.DepartDate).Days;
                if (requestDays <= 0)
                    return 0;

                // if request in first half of year
                if(isFirstHalf)
                {
                    int availableLeaveInFirstHalf = firstHalfMaxDays - leaveBalance!.TakenDays;
                    if (requestDays <= availableLeaveInFirstHalf)
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
                        leaveBalance.TakenDays += requestDays;

                        await context.SaveChangesAsync(cancellationToken);

                        return 1;
                    }
                    else
                        return 0;
                }
                else
                {
                    // second half,
                    int totalTaken = leaveBalance!.TakenDays;
                    int availableLeaveInSecondHalf = secondHalfMaxDays - totalTaken;

                    if (requestDays <= secondHalfMaxDays)
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
                        leaveBalance.TakenDays += requestDays;

                        await context.SaveChangesAsync(cancellationToken);
                        return 1;
                    }
                    else
                        return 0;

                }
            }
            return 0;
        }
    }
}
