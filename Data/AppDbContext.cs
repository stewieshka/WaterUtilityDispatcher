using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterUtilityDispatcher.Data.Configurations;
using WaterUtilityDispatcher.Domain.BrigadeRoot;
using WaterUtilityDispatcher.Domain.IncidentRoot;
using WaterUtilityDispatcher.Domain.UserMaterialRoot;
using WaterUtilityDispatcher.Domain.WorkerRoot;

namespace WaterUtilityDispatcher.Data;
public class AppDbContext : DbContext
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Brigade> Brigades { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<UsedMaterial> UsedMaterials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql("Server = localhost; Port = 5432; User Id = root; Password = root; Database = water;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
