using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Domain.BrigadeRoot;

namespace WaterUtilityDispatcher.Data.Configurations;
public class BrigadeConfiguration : IEntityTypeConfiguration<Brigade>
{
    public void Configure(EntityTypeBuilder<Brigade> builder)
    {
        builder.ToTable("brigades");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();
    }
}
