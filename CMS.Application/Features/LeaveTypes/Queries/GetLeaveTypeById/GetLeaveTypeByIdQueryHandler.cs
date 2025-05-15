using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.Branches.Queries.GetBranchById;
using CMS.Application.Features.LeaveTypes.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Queries.GetLeaveTypeById
{
    class GetLeaveTypeByIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetLeaveTypeByIdQuery, LeaveTypeDto>
    {

        public async Task<LeaveTypeDto> Handle(GetLeaveTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var leaveType = await context.LeaveType.FirstOrDefaultAsync(b => b.Id == request.id, cancellationToken);
            if (leaveType == null)
                throw new NotFoundException($"Leave Type with the id {request.id} Not found");

            var leaveTypeDto = leaveType.Adapt<LeaveTypeDto>();
            return leaveTypeDto;
        }
    }
}
