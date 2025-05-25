using CMS.Application.Common.CQRS;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.LeaveTypes.Commands.DeleteLeaveType
{
    public record DeleteLeaveTypeCommand(int Id) : ICommand<bool>;
    public class DeleteLeaveTypeCommandHandler(IApplicationDbContext context)
        : ICommandHandler<DeleteLeaveTypeCommand, bool>
    {
        public async Task<bool> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await context.LeaveType.FindAsync(request.Id, cancellationToken);
            if (leaveType != null)
            {
                await context.LeaveType.Where(b => b.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
                return false;
        }
    }
   
}
