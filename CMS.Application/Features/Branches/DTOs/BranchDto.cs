using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Branches.DTOs
{
    public record BranchDto(
        int Id,
        string Name,
        string? Leader
        );
    
    
}
