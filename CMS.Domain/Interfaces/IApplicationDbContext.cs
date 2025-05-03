using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;
using CMS.Domain.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Domain.Interfaces
{
    public interface IApplicationDbContext
    {
         DbSet<Person> Persons { get;  }
         DbSet<Soldier> Soldiers { get;  }
         DbSet<Officer> Officers { get; }
         DbSet<Nco> Ncos { get; }
         DbSet<Branch> Branches { get; }
         DbSet<Rank> Ranks { get; }
         DbSet<PersonType> PersonTypes { get; }
         DbSet<LeaveBalance> LeaveBalance { get; }
         DbSet<LeaveType> LeaveType { get; }
         DbSet<Leave> Leave { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
