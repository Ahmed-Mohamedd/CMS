using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Leaves.Utilities;
using CMS.Domain.Entities;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Leaves.Commands.AddLeave
{
    public class AddAllTypesOfLeaveExceptAnnualandCasualCommandHandler(IApplicationDbContext context) : ICommandHandler<AddAllTypesOfLeaveExceptAnnualandCasualCommand, int>
    {
        public async Task<int> Handle(AddAllTypesOfLeaveExceptAnnualandCasualCommand request, CancellationToken cancellationToken)
        {
            if(request.Dto.PersonIds.Count > 0)
            {
                foreach(var personId in request.Dto.PersonIds)
                {
                    // get the person himself if founded
                    var person = await context.Persons
                                              .Include(p => p.LeaveBalances)
                                              .FirstOrDefaultAsync(p => p.Id == personId, cancellationToken);

                    // check if leavetype is allowed for person 
                    if (await Helper.IsLeaveTypeAllowedForPerson(request.Dto.LeaveTypeId, person!.PersonTypeId, context))
                    {
                        // check if leaveType is any thing rather than (emergency & annual ) so i 'll need only to add in leaves table and update IsAbsent in persons table .
                        var leave = new Leave
                        {
                            PersonId = personId,
                            LeaveTypeId = request.Dto.LeaveTypeId,
                            DepartDate = request.Dto.DepartDate,
                            DepartHour = request.Dto.DepartHour,
                            ReturnDate = request.Dto.ReturnDate,
                            ReturnHour = request.Dto.ReturnHour,
                            Notes = request.Dto.Notes,
                        };
                        await context.Leave.AddAsync(leave);
                        person.IsAbsent = true; // set the person to be absent
                        await context.SaveChangesAsync(cancellationToken); // save the leave in leaves table

                    }
                    else
                    {
                        return 0;
                    }
                }

                return 1;
            }
            return 0;
        }


    }
}
