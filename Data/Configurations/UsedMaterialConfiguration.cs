using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Domain.UserMaterialRoot;

namespace WaterUtilityDispatcher.Data.Configurations;
public class UsedMaterialConfiguration : IEntityTypeConfiguration<UsedMaterial>
{
    public void Configure(EntityTypeBuilder<UsedMaterial> builder)
    {
        builder.ToTable("used_materials");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnName("amount")
            .IsRequired();
    }
}
