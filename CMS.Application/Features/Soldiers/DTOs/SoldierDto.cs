using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Soldiers.DTOs
{
    public record SoldierDto(
        Guid PersonId,
        string SoldierName);
   
}
