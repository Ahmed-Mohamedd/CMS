using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Ranks.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Queries.GetRanks
{
    public record GetRanksQuery
        (
        int PageIndex = 1,
        int PageSize = 5
        ) : IQuery<PaginatedResult<RankDto>>;
   
}
