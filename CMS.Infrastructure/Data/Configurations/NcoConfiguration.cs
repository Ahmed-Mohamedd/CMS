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
    public class NcoConfiguration : IEntityTypeConfiguration<Nco>
    {
        public void Configure(EntityTypeBuilder<Nco> builder)
        {
            builder.ToTable("Ncos");
            builder.HasOne(s => s.Person)
               .WithOne()
               .HasForeignKey<Nco>(s => s.PersonId);

        }
    }
}
