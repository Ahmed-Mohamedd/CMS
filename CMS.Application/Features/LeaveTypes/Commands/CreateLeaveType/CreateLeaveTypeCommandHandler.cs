using CMS.Application.Common.CQRS;
using CMS.Application.Features.Branches.Commands.CreateBranch;
using CMS.Application.Features.LeaveTypes.DTOs;
using CMS.Domain.Entities;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CreateLeaveTypeCommand, CreateLeaveTypeResult>
    {
        public async Task<CreateLeaveTypeResult> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = new LeaveType
            {
                Name = request.LeaveType.Name,
                Description = request.LeaveType.Description,
                IsForNCO = request.LeaveType.IsForNCO,
                IsForOfficer = request.LeaveType.IsForOfficer,
                IsForSoldier = request.LeaveType.IsForSoldier
            };
            await context.LeaveType.AddAsync(leaveType);
            await context.SaveChangesAsync(cancellationToken);
            return new CreateLeaveTypeResult(leaveType.Id);
        }
    }
}
