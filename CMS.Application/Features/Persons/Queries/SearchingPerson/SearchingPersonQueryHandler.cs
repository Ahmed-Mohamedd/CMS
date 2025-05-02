using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Persons.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS.Application.Features.Persons.Queries.SearchingPerson
{
    public record SearchingPersonQuery(string SearchQuery) :IQuery<IEnumerable<PersonDto>>;
    public class SearchingPersonQueryHandler(IApplicationDbContext context) : IQueryHandler<SearchingPersonQuery, IEnumerable<PersonDto>>
    {
        public async Task<IEnumerable<PersonDto>> Handle(SearchingPersonQuery request, CancellationToken cancellationToken)
        {
                var persons = await context.Persons
                .Where(p =>
                            p.FullName.Contains(request.SearchQuery) ||
                            p.MilitaryNumber.Contains(request.SearchQuery) ||
                            p.NationalId.Contains(request.SearchQuery))
                .ProjectToType<PersonDto>()
                .ToListAsync();

            return persons;
            //return persons.Adapt<IEnumerable<PersonDto>>();
        }
    }
}
