using CMS.Application.Common.CQRS;
using CMS.Application.Features.Soldiers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Soldiers.Queries
{
    public record GetSoldierByMilitaryServiceEndDateQuery(
        DateTime MilitaryServiceEndDate) : IQuery<SoldierDto>;
    
}
