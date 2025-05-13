using CMS.Application.Common.CQRS;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Commands.DeleteBranch
{
    public record DeleteBranchCommand(int Id): ICommand<bool>;
    public class DeleteBranchCommandHandler(IApplicationDbContext context)
        : ICommandHandler<DeleteBranchCommand, bool>
    {
        public async Task<bool> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await context.Branches.FindAsync(request.Id, cancellationToken);
            if(branch != null)
            {
                await context.Branches.Where(b => b.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
                return false;
        }
    }
}
