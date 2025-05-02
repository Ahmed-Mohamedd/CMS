using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Persons.DTOs;

namespace CMS.Application.Features.Persons.Queries.GetPersonById
{
    public record GetPersonByIdQuery(Guid Id) : IQuery<PersonDto>;

    
}
