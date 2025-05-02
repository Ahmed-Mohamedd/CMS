using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Domain.Interfaces;
using CMS.Application.Features.Persons.DTOs;
using CMS.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Persons.Queries.GetPersonById
{
    public class GetPersonByIdHandler(IApplicationDbContext context) : IQueryHandler<GetPersonByIdQuery, PersonDto>
    {
        public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await context.Persons
            .Include(p => p.PersonType)
            .Include(p => p.Branch)
            .Include(p => p.Rank)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            //if (person == null)
            //    throw new NotFoundException(nameof(Person), request.Id);

           return person.Adapt<PersonDto>();
        }
    }
}
