using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Branches.Commands.CreateBranch;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.Branches.Queries.GetBranchs;
using CMS.Application.Features.LeaveTypes.Commands.CreateLeaveType;
using CMS.Application.Features.LeaveTypes.DTOs;
using CMS.Domain.Entities;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Queries.GetLeaveTypes
{
    class GetLeaveTypesQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetLeaveTypesQuery, PaginatedResult<LeaveTypeDto>>
    {

        public async Task<PaginatedResult<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            var query = context.LeaveType.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query
                             .OrderBy(b => b.Name)
                             .Skip((request.PageIndex - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ProjectToType<LeaveTypeDto>()
                             .ToListAsync(cancellationToken);

            return new PaginatedResult<LeaveTypeDto>(request.PageIndex, request.PageSize, totalCount, data);
        }
    }
}
