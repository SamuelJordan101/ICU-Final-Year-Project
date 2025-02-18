﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tracker.API.Model;

#nullable disable

namespace Tracker.API
{
    public partial class TrackerAppContext : DbContext
    {
        public TrackerAppContext()
        {
        }

        public TrackerAppContext(DbContextOptions<TrackerAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Cpax> Cpaxes { get; set; }
        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<ExercisePlan> ExercisePlans { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Step> Steps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=TrackerApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.ToTable("Achievement");

                entity.HasIndex(e => e.Id, "UQ__Achievem__3214EC26C1A86F65")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Achievement1)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("Achievement");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");
            });

            modelBuilder.Entity<Cpax>(entity =>
            {
                entity.ToTable("CPAX");

                entity.HasIndex(e => e.Id, "UQ__CPAX__3214EC2624D9AE97")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Exercise__3214EC2609382312")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ExerciseDescription).HasColumnType("text");

                entity.Property(e => e.ExerciseName)
                    .IsRequired()
                    .HasColumnType("text");

            });

            modelBuilder.Entity<ExercisePlan>(entity =>
            {
                entity.ToTable("ExercisePlan");

                entity.HasIndex(e => e.Id, "UQ__Exercise__3214EC26A992CA99")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ExerciseId).HasColumnName("ExerciseID");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Goals__3214EC268A2D3F53")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Goal1)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("Goal");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.HasIndex(e => e.Id, "UQ__Image__3214EC26C5FC99FC")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImageData)
                    .IsRequired()
                    .HasColumnType("image");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.HasIndex(e => e.PatientId, "UQ__Patient__970EC34792437C88")
                    .IsUnique();

                entity.Property(e => e.PatientId)
                    .ValueGeneratedNever()
                    .HasColumnName("PatientID");

                entity.Property(e => e.Admission).HasColumnType("date");

                entity.Property(e => e.GoalCpax).HasColumnName("GoalCPAX");

                entity.Property(e => e.Hospital)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Ward)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.HasIndex(e => e.Id, "UQ__Steps__3214EC26BB3E9C6E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Step1)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("Step");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
