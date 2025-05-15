using CMS.Application.Common.CQRS;
using CMS.Application.Common.Exceptions;
using CMS.Application.Features.Branches.DTOs;
using CMS.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Queries.GetBranchById
{
    class GetBranchByIdHandler(IApplicationDbContext context) : IQueryHandler<GetBranchByIdQuery, BranchDto>
    {
        public async Task<BranchDto> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var branch = await context.Branches
                               .AsNoTracking()
                               .Include(x => x.Leader)
                               .ThenInclude(x => x.Person)
                               .FirstOrDefaultAsync(b => b.Id == request.id, cancellationToken);
            if (branch == null)
                throw new NotFoundException($"Branch with the id {request.id} Not found");

            var branchDto = branch.Adapt<BranchDto>();
            return branchDto;
        }
    }
}
