using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Leaves.DTOs
{
    public record AddCasualLeaveDto(

    Guid PersonId,
    DateTime DepartDate,
    int? DepartHour,
    DateTime ReturnDate,
    int? ReturnHour,
    string? Notes
    );
   
}
