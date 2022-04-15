﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shekhovtsova_backend.Models;

namespace Shekhovtsova_backend.Migrations
{
    [DbContext(typeof(AuthContext))]
    partial class AuthContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Shekhovtsova_backend.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Region")
                        .HasColumnType("int");

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Shekhovtsova_backend.Models.Energy", b =>
                {
                    b.Property<int>("EnergyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EcologyDamage")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("EnergyID");

                    b.ToTable("Energies");

                    b.HasData(
                        new
                        {
                            EnergyID = 1,
                            EcologyDamage = 6,
                            Type = 0
                        },
                        new
                        {
                            EnergyID = 2,
                            EcologyDamage = 3,
                            Type = 1
                        },
                        new
                        {
                            EnergyID = 3,
                            EcologyDamage = 10,
                            Type = 2
                        });
                });

            modelBuilder.Entity("Shekhovtsova_backend.Models.EnergyCard", b =>
                {
                    b.Property<int>("EnergyCardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Consumption")
                        .HasColumnType("float");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<int>("EnergyID")
                        .HasColumnType("int");

                    b.Property<double>("Production")
                        .HasColumnType("float");

                    b.HasKey("EnergyCardID");

                    b.HasIndex("CountryID");

                    b.HasIndex("EnergyID");

                    b.ToTable("EnergyCards");
                });

            modelBuilder.Entity("Shekhovtsova_backend.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Shekhovtsova_backend.Models.EnergyCard", b =>
                {
                    b.HasOne("Shekhovtsova_backend.Models.Country", null)
                        .WithMany("EnergyBalance")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shekhovtsova_backend.Models.Energy", "Energy")
                        .WithMany()
                        .HasForeignKey("EnergyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Energy");
                });

            modelBuilder.Entity("Shekhovtsova_backend.Models.Country", b =>
                {
                    b.Navigation("EnergyBalance");
                });
#pragma warning restore 612, 618
        }
    }
}
