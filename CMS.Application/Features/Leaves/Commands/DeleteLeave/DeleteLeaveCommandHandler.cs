using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Common.CQRS;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Leaves.Commands.DeleteLeave
{
    public record DeleteLeaveCommand(int Id) : ICommand<bool>;
    public class DeleteLeaveCommandHandler(IApplicationDbContext context ):ICommandHandler<DeleteLeaveCommand, bool>
    {
        public async Task<bool> Handle(DeleteLeaveCommand request, CancellationToken cancellationToken)
        {
            var leave = await context.Leave.FindAsync(request.Id, cancellationToken);
            if (leave != null)
            {
                await context.Leave.Where(l => l.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
                return false;
        }
    }
   
}
