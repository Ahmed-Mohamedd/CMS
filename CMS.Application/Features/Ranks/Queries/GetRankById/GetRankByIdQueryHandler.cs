using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Ranks.DTOs;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Queries.GetRankById
{
    public class GetRankByIdQueryHandler(IApplicationDbContext context)
        : IQueryHandler<GetRankByIdQuery, RankDto>
    {
        public async Task<RankDto> Handle(GetRankByIdQuery request, CancellationToken cancellationToken)
        {
            var rank = await context.Ranks.FirstOrDefaultAsync(r => r.Id == request.id, cancellationToken);

            if (rank == null)
                throw new NotFoundException($"Rank with the id {request.id} Not found");

            var rankDto = rank.Adapt<RankDto>();
            return rankDto;
        }
    }
}
