using CMS.Application.Features.LeaveTypes.DTOs;
using CMS.Domain.Entities.LeaveEntities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Mapping
{
    public class LeaveTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LeaveType, LeaveTypeDto>();
        }
    }
}
