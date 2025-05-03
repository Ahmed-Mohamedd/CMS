using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Data.Configurations
{
    public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.DepartDate)
                   .IsRequired();
            
            builder.Property(l => l.ReturnDate)
                .IsRequired();

            builder.HasOne(l => l.Person)
                   .WithMany(p => p.Leaves)
                   .HasForeignKey(l => l.PersonId);

            builder.HasOne(l => l.LeaveType)
                   .WithMany(lt => lt.Leaves)
                   .HasForeignKey(l => l.LeaveTypeId);
        }
    }
}
