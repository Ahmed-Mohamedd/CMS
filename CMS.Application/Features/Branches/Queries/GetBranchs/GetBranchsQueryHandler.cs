using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Branches.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Queries.GetBranchs
{
    public class GetBranchsQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetBranchsQuery, PaginatedResult<BranchDto>>
    {
        public async Task<PaginatedResult<BranchDto>> Handle(GetBranchsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Branches.AsQueryable()
                                   .Include(x => x.Leader)
                                   .ThenInclude(x => x.Person);

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query
                             .OrderBy(b => b.Name)
                             .Skip((request.PageIndex - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ProjectToType<BranchDto>()
                             .ToListAsync(cancellationToken);

            return new PaginatedResult<BranchDto>(request.PageIndex, request.PageSize, totalCount,data);
        }
    }
}
