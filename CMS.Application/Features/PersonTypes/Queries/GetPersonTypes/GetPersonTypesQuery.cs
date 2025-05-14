using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.PersonTypes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.PersonTypes.Queries.GetPersonTypes
{
    public record GetPersonTypesQuery(
        int PageIndex = 1,
        int PageSize = 5
        ): IQuery<PaginatedResult<PersonTypeDto>>;

    
}
