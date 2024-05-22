﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WaterUtilityDispatcher.Data;

#nullable disable

namespace WaterUtilityDispatcher.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240517141020_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.BrigadeRoot.Brigade", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("brigades", (string)null);
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.IncidentRoot.Incident", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("address");

                    b.Property<Guid?>("BrigadeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("priority");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("status");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("BrigadeId");

                    b.ToTable("incidents", (string)null);
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.UserMaterialRoot.UsedMaterial", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer")
                        .HasColumnName("amount");

                    b.Property<Guid?>("IncidentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IncidentId");

                    b.ToTable("used_materials", (string)null);
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.WorkerRoot.Worker", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birth_day");

                    b.Property<Guid?>("BrigadeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EmploymentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("employment_date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("middle_name");

                    b.Property<decimal>("Salary")
                        .HasColumnType("numeric")
                        .HasColumnName("salary");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("BrigadeId");

                    b.ToTable("workers", (string)null);
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.IncidentRoot.Incident", b =>
                {
                    b.HasOne("WaterUtilityDispatcher.Domain.BrigadeRoot.Brigade", "Brigade")
                        .WithMany()
                        .HasForeignKey("BrigadeId");

                    b.Navigation("Brigade");
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.UserMaterialRoot.UsedMaterial", b =>
                {
                    b.HasOne("WaterUtilityDispatcher.Domain.IncidentRoot.Incident", "Incident")
                        .WithMany("UsedMaterials")
                        .HasForeignKey("IncidentId");

                    b.Navigation("Incident");
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.WorkerRoot.Worker", b =>
                {
                    b.HasOne("WaterUtilityDispatcher.Domain.BrigadeRoot.Brigade", "Brigade")
                        .WithMany("Workers")
                        .HasForeignKey("BrigadeId");

                    b.Navigation("Brigade");
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.BrigadeRoot.Brigade", b =>
                {
                    b.Navigation("Workers");
                });

            modelBuilder.Entity("WaterUtilityDispatcher.Domain.IncidentRoot.Incident", b =>
                {
                    b.Navigation("UsedMaterials");
                });
#pragma warning restore 612, 618
        }
    }
}
