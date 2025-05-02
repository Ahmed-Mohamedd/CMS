using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Common.Pagination
{
    public record PaginationRequest(int PageIndex = 1, int PageSize = 20);
}
