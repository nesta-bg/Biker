﻿// <auto-generated />
using System;
using Biker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Biker.Migrations
{
    [DbContext(typeof(BikerDbContext))]
    [Migration("20191202142431_CreatePhotosTable")]
    partial class CreatePhotosTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Biker.Core.Models.Bike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsRegistered");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("ModelId");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Bikes");
                });

            modelBuilder.Entity("Biker.Core.Models.BikeFeature", b =>
                {
                    b.Property<int>("BikeId");

                    b.Property<int>("FeatureId");

                    b.HasKey("BikeId", "FeatureId");

                    b.HasIndex("FeatureId");

                    b.ToTable("BikeFeatures");
                });

            modelBuilder.Entity("Biker.Core.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Biker.Core.Models.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("Biker.Core.Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MakeId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("MakeId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("Biker.Core.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BikeId");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("BikeId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Biker.Core.Models.Bike", b =>
                {
                    b.HasOne("Biker.Core.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Biker.Core.Models.Contact", "Contact", b1 =>
                        {
                            b1.Property<int?>("BikeId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Email")
                                .HasColumnName("ContactEmail")
                                .HasMaxLength(255);

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnName("ContactName")
                                .HasMaxLength(255);

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnName("ContactPhone")
                                .HasMaxLength(255);

                            b1.ToTable("Bikes");

                            b1.HasOne("Biker.Core.Models.Bike")
                                .WithOne("Contact")
                                .HasForeignKey("Biker.Core.Models.Contact", "BikeId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Biker.Core.Models.BikeFeature", b =>
                {
                    b.HasOne("Biker.Core.Models.Bike", "Bike")
                        .WithMany("Features")
                        .HasForeignKey("BikeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Biker.Core.Models.Feature", "Feature")
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Biker.Core.Models.Model", b =>
                {
                    b.HasOne("Biker.Core.Models.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Biker.Core.Models.Photo", b =>
                {
                    b.HasOne("Biker.Core.Models.Bike")
                        .WithMany("Photos")
                        .HasForeignKey("BikeId");
                });
#pragma warning restore 612, 618
        }
    }
}