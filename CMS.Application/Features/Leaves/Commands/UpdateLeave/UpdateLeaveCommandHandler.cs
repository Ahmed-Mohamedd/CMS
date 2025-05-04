using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Leaves.Utilities;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Leaves.Commands.UpdateLeave
{
    public class UpdateLeaveCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateLeaveCommand, bool>
    {
        public async Task<bool> Handle(UpdateLeaveCommand request, CancellationToken cancellationToken)
        {
            var person = await context.Persons
                                      .FirstOrDefaultAsync(p => p.Id == request.Dto.PersonId, cancellationToken);

            var leave = await context.Leave.FindAsync(request.Id, cancellationToken);
            if (leave != null && await Helper.IsLeaveTypeAllowedForPerson(request.Dto.LeaveTypeId , person.PersonTypeId , context))
            {
                leave.PersonId = request.Dto.PersonId;
                leave.LeaveTypeId = request.Dto.LeaveTypeId;
                leave.DepartDate = request.Dto.DepartDate;
                leave.DepartHour = request.Dto.DepartHour;
                leave.ReturnDate = request.Dto.ReturnDate;
                leave.ReturnHour = request.Dto.ReturnHour;
                leave.Notes = request.Dto.Notes;

                person.IsAbsent = true; // set the person as absent
                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
                return false;
        }
    }
}
