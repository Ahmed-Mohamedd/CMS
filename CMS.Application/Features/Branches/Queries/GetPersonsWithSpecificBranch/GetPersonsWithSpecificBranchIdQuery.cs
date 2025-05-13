using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Branches.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMS.Application.Features.Branches.Queries.GetPersonsWithSpecificBranch
{
    public record GetPersonsWithSpecificBranchIdQuery(
        int Id,
        int PageIndex = 1,
        int PageSize = 5
        ): IQuery<PaginatedResult<BranchWithPersonsDto>>;
    
}
