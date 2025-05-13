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

namespace CMS.Application.Features.Branches.Queries.GetPersonsWithSpecificBranch
{
    public class GetPersonsWithSpecificBranchIdQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetPersonsWithSpecificBranchIdQuery, PaginatedResult<BranchWithPersonsDto>>
    {
        public async Task<PaginatedResult<BranchWithPersonsDto>> Handle(GetPersonsWithSpecificBranchIdQuery request, CancellationToken cancellationToken)
        {
            var query = context.Branches.Where(b => b.Id == request.Id)
                               .Include(x => x.Leader)
                               .ThenInclude(x => x.Person);

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query.OrderBy(x => x.Name)
                            .Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ProjectToType<BranchWithPersonsDto>()
                            .ToListAsync(cancellationToken);

            return new PaginatedResult<BranchWithPersonsDto>(request.PageIndex, request.PageSize, totalCount, data);
        }
    }
}
