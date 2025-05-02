using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Features.Persons.DTOs;
using CMS.Domain.Entities;
using Mapster;

namespace CMS.Application.Features.Persons.Mapping
{
    public class PersonMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Person, PersonDto>()
            .Map(dest => dest.PersonType, src => src.PersonType.Name)
            .Map(dest =>  dest.Rank , src => src.Rank.Name)
            .Map(dest => dest.Branch, src => src.Branch.Name);
        }
    }
}
