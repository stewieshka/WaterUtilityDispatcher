using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Domain.WorkerRoot;

namespace WaterUtilityDispatcher.Data.Configurations;
public sealed class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable("workers");

        builder.HasKey(x => x.Id)
            .HasName("id");

        builder.Property(x => x.FirstName)
            .HasColumnName("first_name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnName("last_name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder.Property(x => x.MiddleName)
            .HasColumnName("middle_name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder.Property(x => x.BirthDay)
            .HasColumnName("birth_day")
            .IsRequired();

        builder.Property(x => x.EmploymentDate)
            .HasColumnName("employment_date")
            .IsRequired();

        builder.Property(x => x.Salary)
            .HasColumnName("salary")
            .IsRequired();

        builder.Ignore(x => x.BrigadeName);
    }
}
