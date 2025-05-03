using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;
using CMS.Domain.Entities.LeaveEntities;
using CMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Soldier> Soldiers => Set<Soldier>();
        public DbSet<Nco> Ncos => Set<Nco>();
        public DbSet<Officer> Officers => Set<Officer>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Rank> Ranks => Set<Rank>();
        public DbSet<PersonType> PersonTypes => Set<PersonType>();
        public DbSet<LeaveType> LeaveType => Set<LeaveType>();
        public DbSet<Leave> Leave => Set<Leave>();
        public DbSet<LeaveBalance> LeaveBalance => Set<LeaveBalance>();

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
