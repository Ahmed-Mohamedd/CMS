using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Branches.DTOs;
using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Commands.UpdateBranch
{
    public record UpdateBranchCommand(int Id,UpdateBranchDto Branch) : ICommand<bool>;

    public class UpdateBranchCommandHandler(IApplicationDbContext context)
               : ICommandHandler<UpdateBranchCommand, bool>
    {
        public async Task<bool> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await context.Branches.FindAsync(request.Id, cancellationToken);

            if (branch == null)
                throw new NotFoundException($"No Branch Found with Id {request.Id}");

            branch.Name = request.Branch.Name;
            branch.LeaderId = request.Branch.LeaderId;

            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }


}
