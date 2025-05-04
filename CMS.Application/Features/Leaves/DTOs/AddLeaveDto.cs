using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;

namespace CMS.Application.Features.Leaves.DTOs
{
    public record  AddLeaveDto
    (
        Guid PersonId,
     int LeaveTypeId,

     DateTime DepartDate,
     int? DepartHour,
     DateTime ReturnDate,
     int? ReturnHour,

     string? Notes
    );
}
