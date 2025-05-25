using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.LeaveTypes.DTOs;
using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Commands.UpdateLeaveType
{
    public record UpdateLeaveTypeCommand(int Id, CreateLeaveTypeDto LeaveType) : ICommand<bool>;

    public class UpdateLeaveTypeCommandHandler(IApplicationDbContext context)
               : ICommandHandler<UpdateLeaveTypeCommand, bool>
    {
        public async Task<bool> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await context.LeaveType.FindAsync(request.Id, cancellationToken);

            if (leaveType == null)
                throw new NotFoundException($"No Leave Type Found with Id {request.Id}");

            leaveType.Name = request.LeaveType.Name;
            leaveType.Description = request.LeaveType.Description;
            leaveType.IsForOfficer = request.LeaveType.IsForOfficer;
            leaveType.IsForNCO = request.LeaveType.IsForNCO;
            leaveType.IsForSoldier = request.LeaveType.IsForSoldier;

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
   
}
