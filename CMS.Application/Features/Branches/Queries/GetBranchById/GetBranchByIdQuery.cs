using CMS.Application.Common.CQRS;
using CMS.Application.Features.Branches.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.Queries.GetBranchById
{
    public record GetBranchByIdQuery(int id) : IQuery<BranchDto>;
    
}
