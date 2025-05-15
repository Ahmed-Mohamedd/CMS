using CMS.Application.Common.CQRS;
using CMS.Application.Features.Ranks.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Queries.GetRankById
{
    public record GetRankByIdQuery(int id) : IQuery<RankDto>;
   
}
