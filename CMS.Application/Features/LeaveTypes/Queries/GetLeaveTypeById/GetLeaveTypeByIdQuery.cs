using CMS.Application.Common.CQRS;
using CMS.Application.Features.Branches.DTOs;
using CMS.Application.Features.LeaveTypes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Queries.GetLeaveTypeById
{
    public record  GetLeaveTypeByIdQuery(int id) : IQuery<LeaveTypeDto>;
    
    
}
