using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Ranks.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Queries.GetRanks
{
    public class GetRanksQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetRanksQuery, PaginatedResult<RankDto>>
    {
        public async Task<PaginatedResult<RankDto>> Handle(GetRanksQuery request, CancellationToken cancellationToken)
        {
            var query =  context.Ranks.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query
                             .OrderBy(x => x.Name)
                             .Skip((request.PageIndex - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ProjectToType<RankDto>()
                             .ToListAsync(cancellationToken);

            return new PaginatedResult<RankDto>(request.PageIndex, request.PageSize, totalCount, data);
            
        }
    }
}
