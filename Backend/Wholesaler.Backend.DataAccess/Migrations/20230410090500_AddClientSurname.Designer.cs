﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wholesaler.Backend.DataAccess;

#nullable disable

namespace Wholesaler.Backend.DataAccess.Migrations
{
    [DbContext(typeof(WholesalerContext))]
    [Migration("20230410090500_AddClientSurname")]
    partial class AddClientSurname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Stop")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WorkTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("WorkTaskId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Requirement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Requirements");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Workday", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Stop")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Workdays");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.WorkTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("WorkTasks");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Activity", b =>
                {
                    b.HasOne("Wholesaler.Backend.DataAccess.Models.Person", "Person")
                        .WithMany("Activities")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wholesaler.Backend.DataAccess.Models.WorkTask", "WorkTask")
                        .WithMany("Activities")
                        .HasForeignKey("WorkTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("WorkTask");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Requirement", b =>
                {
                    b.HasOne("Wholesaler.Backend.DataAccess.Models.Client", "Client")
                        .WithMany("Requirements")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Workday", b =>
                {
                    b.HasOne("Wholesaler.Backend.DataAccess.Models.Person", "Person")
                        .WithMany("Workdays")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.WorkTask", b =>
                {
                    b.HasOne("Wholesaler.Backend.DataAccess.Models.Person", "Person")
                        .WithMany("WorkTasks")
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Client", b =>
                {
                    b.Navigation("Requirements");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.Person", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("WorkTasks");

                    b.Navigation("Workdays");
                });

            modelBuilder.Entity("Wholesaler.Backend.DataAccess.Models.WorkTask", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
