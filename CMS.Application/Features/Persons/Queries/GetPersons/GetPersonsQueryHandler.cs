using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Persons.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Persons.Queries.GetPersons
{
    public class GetPersonsQueryHandler(IApplicationDbContext context) : IQueryHandler<GetPersonsQuery, PaginatedResult<PersonDto>>
    {
        public async Task<PaginatedResult<PersonDto>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var query = context.Persons.AsNoTracking().AsQueryable();

            if (request.BranchId.HasValue)
                query = query.Where(p => p.BranchId == request.BranchId.Value);

            if (request.PersonTypeId.HasValue)
                query = query.Where(p => p.PersonTypeId == request.PersonTypeId.Value);

            if (request.RankId.HasValue)
                query = query.Where(p => p.RankId == request.RankId.Value);

            if (request.NationalId != null)
                query = query.Where(p => p.NationalId.Contains(request.NationalId));

            if (request.MilitaryNumber != null)
                query = query.Where(p => p.MilitaryNumber.Contains( request.MilitaryNumber));

            var totalCount = await query.CountAsync(cancellationToken);

            var data = await query
                .OrderBy(p => p.FullName)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectToType<PersonDto>()
                .ToListAsync(cancellationToken);

            return new PaginatedResult<PersonDto>(request.PageIndex, request.PageSize, totalCount, data);
        }
    }
}
