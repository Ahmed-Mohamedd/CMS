using CMS.Application.Features.Ranks.DTOs;
using CMS.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks
{
    public class RankMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Rank, RankDto>();
        }
    }
}
