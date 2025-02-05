﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VirtualHerbarium.Backend.Entities;

namespace VirtualHerbarium.Backend.Migrations
{
    [DbContext(typeof(VirtualHerbariumDbContext))]
    partial class VirtualHerbariumDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VirtualHerbarium.Backend.Entities.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDraft")
                        .HasColumnType("bit");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Mjesto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Porodica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Red")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sinonim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staniste")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrivijalniNaziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vrsta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Biljke");
                });

            modelBuilder.Entity("VirtualHerbarium.Backend.Entities.PlantImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BiljkaId")
                        .HasColumnType("int");

                    b.Property<string>("Slika")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UPrirodi")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BiljkaId");

                    b.ToTable("SlikeBiljaka");
                });

            modelBuilder.Entity("VirtualHerbarium.Backend.Entities.PlantLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BiljkaId")
                        .HasColumnType("int");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Mjesto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Staniste")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BiljkaId");

                    b.ToTable("LokacijeBiljaka");
                });

            modelBuilder.Entity("VirtualHerbarium.Backend.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VirtualHerbarium.Backend.Entities.PlantImage", b =>
                {
                    b.HasOne("VirtualHerbarium.Backend.Entities.Plant", "Biljka")
                        .WithMany("SlikeBiljaka")
                        .HasForeignKey("BiljkaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VirtualHerbarium.Backend.Entities.PlantLocation", b =>
                {
                    b.HasOne("VirtualHerbarium.Backend.Entities.Plant", "Biljka")
                        .WithMany("LokacijeBiljaka")
                        .HasForeignKey("BiljkaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
