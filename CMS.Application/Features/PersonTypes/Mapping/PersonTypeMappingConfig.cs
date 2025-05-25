using CMS.Application.Features.PersonTypes.DTOs;
using CMS.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.PersonTypes.Mapping
{
    public class PersonTypeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PersonType, PersonTypeDto>();
        }
    }
}
