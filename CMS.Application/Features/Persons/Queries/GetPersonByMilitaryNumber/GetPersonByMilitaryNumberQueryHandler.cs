using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Persons.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Persons.Queries.GetPersonByMilitaryNumber
{
    internal class GetPersonByMilitaryNumberQueryHandler(IApplicationDbContext context) : IQueryHandler<GetPersonByMilitaryNumberQuery, PersonDto>
    {
        public async Task<PersonDto> Handle(GetPersonByMilitaryNumberQuery request, CancellationToken cancellationToken)
        {
            var person = await context.Persons
            .Include(p => p.PersonType)
            .Include(p => p.Branch)
            .Include(p => p.Rank)
            .FirstOrDefaultAsync(p => p.MilitaryNumber == request.MilitaryNumber, cancellationToken);

            if (person == null)
                throw new NotFoundException($"person with military number : {request.MilitaryNumber} not found ");

            return person.Adapt<PersonDto>();
        }
    }
}
