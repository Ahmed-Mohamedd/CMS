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
            config.NewConfig<Branch, BranchDto>()
                .Map(des => des.Leader, src => src.Leader.Person.FullName);

            config.NewConfig<Branch, BranchWithPersonsDto>()
                  .Map(des => des.Leader, src => src.Leader.Person.FullName)
                  .Map(des => des.Persons, src => src.Persons.Select(fn => new PersonDto
                                                                        (fn.FullName)));
        }
    }
}
