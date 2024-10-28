﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AdSyst.Advertisments.Infrastructure.Data.Contexts;

#nullable disable

namespace AdSyst.Advertisments.Infrastructure.Data.Migrations
{
    [DbContext(typeof(AdvertismentDbContext))]
    [Migration("20240708095820_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdSyst.Advertisments.Domain.AdvertismentTypes.AdvertismentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AdvertismentTypes");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Advertisments.Advertisment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdvertismentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("_state")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("AdvertismentTypeId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Advertisments");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Advertisments.AdvertismentImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdvertismentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Order")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("AdvertismentId");

                    b.ToTable("AdvertismentImages");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Infrastructure.Messaging.Models.EventMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("OccurredOnUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ProcessedOnUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OccurredOnUtc");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("AdSyst.Advertisments.Infrastructure.Messaging.Models.InboxMessage", b =>
                {
                    b.HasBaseType("AdSyst.Advertisments.Infrastructure.Messaging.Models.EventMessage");

                    b.ToTable("InboxMessages");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Infrastructure.Messaging.Models.OutboxMessage", b =>
                {
                    b.HasBaseType("AdSyst.Advertisments.Infrastructure.Messaging.Models.EventMessage");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Advertisments.Advertisment", b =>
                {
                    b.HasOne("AdSyst.Advertisments.Domain.AdvertismentTypes.AdvertismentType", null)
                        .WithMany()
                        .HasForeignKey("AdvertismentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdSyst.Advertisments.Domain.Categories.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Advertisments.AdvertismentImage", b =>
                {
                    b.HasOne("AdSyst.Advertisments.Domain.Advertisments.Advertisment", null)
                        .WithMany("Images")
                        .HasForeignKey("AdvertismentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Categories.Category", b =>
                {
                    b.HasOne("AdSyst.Advertisments.Domain.Categories.Category", "ParentCategory")
                        .WithMany("Children")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Advertisments.Advertisment", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("AdSyst.Advertisments.Domain.Categories.Category", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}