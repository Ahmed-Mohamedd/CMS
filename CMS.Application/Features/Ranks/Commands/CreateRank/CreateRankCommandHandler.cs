using CMS.Application.Common.CQRS;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Commands.CreateRank
{
    public class CreateRankCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CreateRankCommand, CreateRankCommandResult>
    {
        public async Task<CreateRankCommandResult> Handle(CreateRankCommand request, CancellationToken cancellationToken)
        {
            var rank = new Rank
            {
                Name = request.Rank.Name,
                RankType = request.Rank.RankType
            };

            await context.Ranks.AddAsync(rank);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateRankCommandResult(rank.Id);
            throw new NotImplementedException();
        }
    }
}
