using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Application.Common.Pagination;
using CMS.Application.Features.Leaves.DTOs;

namespace CMS.Application.Features.Leaves.Queries.GetLeaves
{
    public record GetLeavesQuery
    (
        int? LeaveTypeId,
        DateTime? FromDate,
        DateTime? ToDate,
        string? PersonName,
        int? Year,
        int PageIndex = 1,
        int PageSize = 20

        ) :IQuery<PaginatedResult<LeaveDto>>;
}
