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
    public class LeaveBalanceConfiguration : IEntityTypeConfiguration<LeaveBalance>
    {
        public void Configure(EntityTypeBuilder<LeaveBalance> builder)
        {
            builder.HasKey(lb => lb.Id);
            builder.Property(lb => lb.TakenDays).IsRequired();
            builder.Property(lb => lb.TotalDays).IsRequired();
            builder.Property(lb => lb.Year).IsRequired();

            builder.HasOne(lb => lb.LeaveType)
                .WithMany(lt => lt.LeaveBalances)
                .HasForeignKey(lb => lb.LeaveTypeId);

            builder.HasOne(lb => lb.Person)
                .WithMany(p => p.LeaveBalances)
                .HasForeignKey(lb => lb.PersonId);

        }

    }
}
