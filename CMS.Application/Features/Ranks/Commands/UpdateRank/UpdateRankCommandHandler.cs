using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Ranks.DTOs;
using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Commands.UpdateRank
{
    public record UpdateRankCommand(int id, CreateRankDto Rank) : ICommand<bool>;
    public class UpdateRankCommandHandler(IApplicationDbContext context)
        : ICommandHandler<UpdateRankCommand, bool>
    {
        public async Task<bool> Handle(UpdateRankCommand request, CancellationToken cancellationToken)
        {
            var rank = await context.Ranks.FindAsync(request.id, cancellationToken);

            if (rank == null)
                throw new NotFoundException($"No Rank Found with Id {request.id}");

            rank.Name = request.Rank.Name;
            rank.RankType = request.Rank.RankType;

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
