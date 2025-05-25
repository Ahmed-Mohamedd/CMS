using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.PersonTypes.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.PersonTypes.Queries.GetPersonTypes
{
    public class GetPersonTypesQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetPersonTypesQuery, PaginatedResult<PersonTypeDto>>
    {
        public async Task<PaginatedResult<PersonTypeDto>> Handle(GetPersonTypesQuery request, CancellationToken cancellationToken)
        {
            var query = context.PersonTypes.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query
                             .OrderBy(x => x.Name)
                             .Skip((request.PageIndex - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ProjectToType<PersonTypeDto>()
                             .ToListAsync(cancellationToken);

            return new PaginatedResult<PersonTypeDto>(request.PageIndex, request.PageSize, totalCount, data);
        }
    }
}
