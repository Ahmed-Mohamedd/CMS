using CMS.Application.Common.CQRS;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Ranks.Commands.DeleteRank
{
    public record DeleteRankCommand(int id) : ICommand<bool>;
    public class DeleteRankCommandHandler(IApplicationDbContext context)
        : ICommandHandler<DeleteRankCommand, bool>
    {
        public async Task<bool> Handle(DeleteRankCommand request, CancellationToken cancellationToken)
        {
            var rank = await context.Ranks.FindAsync(request.id, cancellationToken);
            if(rank != null)
            {
                await context.Ranks.Where(r => r.Id == request.id).ExecuteDeleteAsync(cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
    }
}
