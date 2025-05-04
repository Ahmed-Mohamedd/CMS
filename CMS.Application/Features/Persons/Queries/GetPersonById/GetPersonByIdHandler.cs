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
using System.Runtime.CompilerServices;

namespace CMS.Application.Features.Persons.Queries.GetPersonById
{
    public class GetPersonByIdHandler(IApplicationDbContext context) : IQueryHandler<GetPersonByIdQuery, PersonDto>
    {
        public async Task<PersonDto> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await context.Persons
            .AsNoTracking()
            .Include(p => p.PersonType)
            .Include(p => p.Branch)
            .Include(p => p.Rank)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            //if (person == null)
            //    throw new NotFoundException(nameof(Person), request.Id);

            var PersonDto = person.Adapt<PersonDto>();
            return await GetPersonWithHisSpecificAttributes(PersonDto , person.PersonTypeId);
        }

        private async Task<PersonDto> GetPersonWithHisSpecificAttributes(PersonDto dto , int PersonTypeId)
        {
            PersonDto SpecifiedDto = null;
            if (PersonTypeId == 1)
            {
                var soldier = await context.Soldiers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.PersonId == dto.Id);
               
                 SpecifiedDto = dto with { MilitaryServiceEndDate = soldier.MilitaryServiceEndDate };
            }
            //else if (PersonTypeId == 2)
            //{
            //    return nco.Adapt<PersonDto>();
            //}
            //else if (PersonTypeId == 3)
            //{
            //    return soldier.Adapt<PersonDto>();
            //}
            //else
            //{

            //}

            return SpecifiedDto;
        }
    }
}
