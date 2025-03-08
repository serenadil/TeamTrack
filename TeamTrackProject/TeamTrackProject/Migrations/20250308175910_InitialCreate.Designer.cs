﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamTrackProject.Models.Infrastrutture;

#nullable disable

namespace TeamTrackProject.Migrations
{
    [DbContext(typeof(TeamTrackDbContext))]
    [Migration("20250308175910_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProgettiUtente", b =>
                {
                    b.Property<int>("IdProgetto")
                        .HasColumnType("int");

                    b.Property<int>("IdUtente")
                        .HasColumnType("int");

                    b.HasKey("IdProgetto", "IdUtente");

                    b.HasIndex("IdUtente");

                    b.ToTable("ProgettiUtente");
                });

            modelBuilder.Entity("TaskProgettoUtente", b =>
                {
                    b.Property<int>("AttivitaId")
                        .HasColumnType("int");

                    b.Property<int>("UtentiId")
                        .HasColumnType("int");

                    b.HasKey("AttivitaId", "UtentiId");

                    b.HasIndex("UtentiId");

                    b.ToTable("TaskProgettoUtente");
                });

            modelBuilder.Entity("TeamTrackProject.Models.Dominio.Progetto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("CodiceAccesso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataFineProgetto")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInizioProgetto")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Progetti");
                });

            modelBuilder.Entity("TeamTrackProject.Models.Dominio.TaskProgetto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataFineTask")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInizioTask")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdProgetto")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrioritàTask")
                        .HasColumnType("int");

                    b.Property<int?>("StatoTask")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdProgetto");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TeamTrackProject.Models.Dominio.Utente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ruolo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("ProgettiUtente", b =>
                {
                    b.HasOne("TeamTrackProject.Models.Dominio.Progetto", null)
                        .WithMany()
                        .HasForeignKey("IdProgetto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamTrackProject.Models.Dominio.Utente", null)
                        .WithMany()
                        .HasForeignKey("IdUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskProgettoUtente", b =>
                {
                    b.HasOne("TeamTrackProject.Models.Dominio.TaskProgetto", null)
                        .WithMany()
                        .HasForeignKey("AttivitaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamTrackProject.Models.Dominio.Utente", null)
                        .WithMany()
                        .HasForeignKey("UtentiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamTrackProject.Models.Dominio.Progetto", b =>
                {
                    b.HasOne("TeamTrackProject.Models.Dominio.Utente", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("TeamTrackProject.Models.Dominio.TaskProgetto", b =>
                {
                    b.HasOne("TeamTrackProject.Models.Dominio.Progetto", "Progetto")
                        .WithMany("Tasks")
                        .HasForeignKey("IdProgetto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Progetto");
                });

            modelBuilder.Entity("TeamTrackProject.Models.Dominio.Progetto", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
