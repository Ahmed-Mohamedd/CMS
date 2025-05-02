using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Infrastructure.Data.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FullName)
                   .IsRequired()
                   .HasMaxLength(200)
                    .IsUnicode(true);

            builder.Property(p => p.MilitaryNumber)
                   .HasMaxLength(13);

            builder.Property(p => p.NationalId)
                   .IsRequired()
                   .HasMaxLength(14);

            builder.Property(p => p.BirthDate)
                   .IsRequired();

            builder.Property(p => p.JoinDateToUnit)
                   .IsRequired();

            builder.Property(p => p.Governorate).HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.District).HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.Village).HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.Street).HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.PhoneNumber).HasMaxLength(20).IsUnicode(true);
            builder.Property(p => p.Email).HasMaxLength(100);

            builder.HasOne(p => p.Branch)
                   .WithMany(b => b.Persons)
                   .HasForeignKey(p => p.BranchId);

            builder.HasOne(p => p.Rank)
                    .WithMany(r => r.Persons)
                    .HasForeignKey(p => p.RankId);

            builder.HasOne(p => p.PersonType)
                   .WithMany(pt => pt.Persons)
                   .HasForeignKey(p => p.PersonTypeId);
        }
    }
}
