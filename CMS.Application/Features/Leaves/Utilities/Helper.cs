using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Features.Leaves.Utilities
{
    public static class Helper
    {
        public async static Task<bool> IsLeaveTypeAllowedForPerson(int leaveTypeId, int personTypeId , IApplicationDbContext context)
        {
            bool flag = false;
            // check if the leave type is allowed for the person
            // this is a stub implementation, replace with actual logic
            if (personTypeId == 1)
            {
                flag = await context.LeaveType
                    .Where(l => l.Id == leaveTypeId && l.IsForSoldier)
                    .AnyAsync();

            }
            else if (personTypeId == 3)
            {
                flag = await context.LeaveType
                    .Where(l => l.Id == leaveTypeId && l.IsForOfficer)
                    .AnyAsync();
            }
            else if (personTypeId == 2)
            {
                flag = await context.LeaveType
                    .Where(l => l.Id == leaveTypeId && l.IsForNCO)
                    .AnyAsync();
            }
            else
            {
                return false;
            }

            return flag;
        }

        public static int ComputeLeaveDays(DateTime DepartDate, DateTime ReturnDate)
            => (ReturnDate - DepartDate).Days + 1;
    }
}
