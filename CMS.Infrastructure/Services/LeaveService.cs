using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ApplicationDbContext _context;

        public LeaveService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task UpdateLeaveStatusAsync(CancellationToken cancellationToken)
        {
            #region using ef core &linq

            //var today = DateTime.Today;

            //var latestLeaves = await _context.Leave
            //    .GroupBy(l => l.PersonId)
            //    .Select(g => g.OrderByDescending(l => l.ReturnDate).FirstOrDefault())
            //    .Where(l => l != null && l.ReturnDate <= today)
            //    .ToListAsync();

            //foreach (var leave in latestLeaves)
            //{
            //    var person = await _context.Persons.FindAsync(leave.PersonId);
            //    if (person != null && person.IsAbsent)
            //    {
            //        person.IsAbsent = false;
            //    }
            //}

            //await _context.SaveChangesAsync();
            #endregion

            await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdatePersonAbsenceStatus", cancellationToken);
        }
    }
}
