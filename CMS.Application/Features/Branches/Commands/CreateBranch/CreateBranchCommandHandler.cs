using CMS.Application.Common.CQRS;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Commands.CreateBranch
{
    public class CreateBranchCommandHandler(IApplicationDbContext context)
        : ICommandHandler<CreateBranchCommand, CreateBranchResult>
    {
        public async Task<CreateBranchResult> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = new Branch
            {
                Name = request.Branch.Name,
                LeaderId = request.Branch.LeaderId
            };
            await context.Branches.AddAsync(branch);
            await context.SaveChangesAsync(cancellationToken);
            return new CreateBranchResult(branch.Id);
        }
    }
}
