﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TesteDotNet2.ProjectControlSystem.Data.SqlServer.Context;

namespace TesteDotNet2.ProjectControlSystem.Data.SqlServer.Migrations
{
    [DbContext(typeof(ProjectControlSystemContext))]
    partial class ProjectControlSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("TesteDotNet2.ProjectControlSystem.Domain.Entities.Developer", b =>
                {
                    b.Property<Guid>("DeveloperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("DeveloperId");

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("TesteDotNet2.ProjectControlSystem.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("ProjectId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TesteDotNet2.ProjectControlSystem.Domain.Entities.TimeSheet", b =>
                {
                    b.Property<Guid>("TimeSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AmountOfHours")
                        .HasColumnType("int");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("DeveloperId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TimeSheetId");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ProjectId");

                    b.ToTable("TimeSheet");
                });

            modelBuilder.Entity("TesteDotNet2.ProjectControlSystem.Domain.Entities.TimeSheet", b =>
                {
                    b.HasOne("TesteDotNet2.ProjectControlSystem.Domain.Entities.Developer", "Developer")
                        .WithMany("TimeSheets")
                        .HasForeignKey("DeveloperId");

                    b.HasOne("TesteDotNet2.ProjectControlSystem.Domain.Entities.Project", "Project")
                        .WithMany("TimeSheets")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Developer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TesteDotNet2.ProjectControlSystem.Domain.Entities.Developer", b =>
                {
                    b.Navigation("TimeSheets");
                });

            modelBuilder.Entity("TesteDotNet2.ProjectControlSystem.Domain.Entities.Project", b =>
                {
                    b.Navigation("TimeSheets");
                });
#pragma warning restore 612, 618
        }
    }
}
