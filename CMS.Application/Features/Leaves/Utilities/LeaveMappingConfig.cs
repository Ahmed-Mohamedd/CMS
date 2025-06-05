using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Features.Leaves.DTOs;
using CMS.Domain.Entities.LeaveEntities;
using Mapster;

namespace CMS.Application.Features.Leaves.Utilities
{
    public class LeaveMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Leave, LeaveDto>()
                .Map(dest => dest.LeaveTypeName, src => src.LeaveType.Name)
                .Map(dest => dest.PersonName, src => src.Person.FullName)
                .Map(dest => dest.MilitaryNumber, src => src.Person.MilitaryNumber);

        }
    }
}
