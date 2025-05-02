using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Features.Persons.DTOs;

namespace CMS.Application.Features.Persons.Queries.GetPersonByMilitaryNumber
{
    public record GetPersonByMilitaryNumberQuery(string MilitaryNumber) : IQuery<PersonDto>;
}
