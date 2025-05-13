using CMS.Application.Features.Soldiers.DTOs;
using CMS.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Soldiers.Mapping
{
    public class SoldiersMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Soldier, SoldierDto>()
                  .Map(des => des.SoldierName, src => src.Person.FullName);
        }
    }
}
