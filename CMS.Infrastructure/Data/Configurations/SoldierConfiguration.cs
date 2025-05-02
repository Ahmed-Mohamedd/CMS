using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Data.Configurations
{
    public class SoldierConfiguration : IEntityTypeConfiguration<Soldier>
    {
        public void Configure(EntityTypeBuilder<Soldier> builder)
        {
            builder.ToTable("Soldiers");
            builder.HasOne(s => s.Person)
                .WithOne()
                .HasForeignKey<Soldier>(s => s.PersonId);

            builder.Property(s => s.MilitaryServiceEndDate)
                   .IsRequired();
        }
    }
}
