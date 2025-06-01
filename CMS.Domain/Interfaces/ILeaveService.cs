using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Interfaces
{
    public interface ILeaveService
    {
        Task UpdateLeaveStatusAsync(CancellationToken cancellationToken);
    }
}
