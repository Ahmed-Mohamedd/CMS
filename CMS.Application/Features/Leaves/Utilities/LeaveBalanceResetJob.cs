using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Features.Leaves.Utilities
{
    public class LeaveBalanceResetJob
    {
        private readonly ApplicationDbContext _context;

        public LeaveBalanceResetJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ResetAnnualLeaveBalancesAsync()
        {
            var annualLeaveType = await _context.LeaveType
                                                .FirstOrDefaultAsync(x => x.Name == "سنوية");

            if (annualLeaveType == null) return;

            var balances = _context.LeaveBalance
                                   .Where(b => b.LeaveTypeId == annualLeaveType.Id);

            await balances.ForEachAsync(b => b.TakenDays = 0);

            await _context.SaveChangesAsync();

            Console.WriteLine($"[Hangfire] Annual leave balances reset at: {DateTime.Now}");
        }
    }
}
