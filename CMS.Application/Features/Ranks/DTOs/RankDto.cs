using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.DTOs
{
    public record RankDto(
        int Id,
        string Name,
        string RankType
        );
   
}
