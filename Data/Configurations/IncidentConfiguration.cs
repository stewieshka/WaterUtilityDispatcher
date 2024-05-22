using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Domain.IncidentRoot;
using WaterUtilityDispatcher.Domain.WorkerRoot;

namespace WaterUtilityDispatcher.Data.Configurations;
public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.ToTable("incidents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .HasColumnName("type")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder.Property(x => x.Address)
            .HasColumnName("address")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired();

        builder.Property(x => x.Priority)
            .HasColumnName("priority")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();
    }
}
