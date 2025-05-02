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

namespace CMS.Application.Features.Persons.Queries.GetPersonByNationalId
{
    public class GetPersonByNationalIdQueryHandler(IApplicationDbContext context) : IQueryHandler<GetPersonByNationalIdQuery, PersonDto>
    {
        public async Task<PersonDto> Handle(GetPersonByNationalIdQuery request, CancellationToken cancellationToken)
        {
            var person = await context.Persons
           .Include(p => p.PersonType)
           .Include(p => p.Branch)
           .Include(p => p.Rank)
           .FirstOrDefaultAsync(p => p.NationalId == request.NationalId, cancellationToken);

            if (person == null)
                throw new NotFoundException($"person with {request.NationalId} not found ");

            return person.Adapt<PersonDto>();
        }
    }
}
