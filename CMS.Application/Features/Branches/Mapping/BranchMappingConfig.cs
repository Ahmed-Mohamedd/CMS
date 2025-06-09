using CMS.Application.Features.Branches.DTOs;
using CMS.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Mapping
{
    public class BranchMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Person, PersonDto>()
                .Map(dest => dest.IsAbsent, src => src.IsAbsent)
                .Map(des => des.PersonType, src => src.PersonType.Name)
                .Map(dest => dest.FullName, src => src.FullName);

            config.NewConfig<Branch, BranchDto>()
                .Map(des => des.Leader, src => src.Leader.Person.FullName);

            config.NewConfig<Branch, BranchWithPersonsDto>()
                  .MapToConstructor(true)
                  .Map(des => des.Leader, src => src.Leader.Person.FullName);

        }
    }
}
