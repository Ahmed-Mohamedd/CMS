using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Persons.DTOs;

namespace CMS.Application.Features.Persons.Queries.GetPersons
{
    public record GetPersonsQuery(
    int? BranchId,
    int? PersonTypeId,
    int? RankId,
    string? NationalId,
    string? MilitaryNumber,
    int PageIndex = 1,
    int PageSize = 20) : IQuery<PaginatedResult<PersonDto>>;
}
