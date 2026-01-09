using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Models;

namespace SUPREA_LOGISTICS.Context;

public partial class MyDBContext : DbContext
{
    public MyDBContext()
    {
    }

    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleDocument> VehicleDocuments { get; set; }

    public virtual DbSet<VehicleLog> VehicleLogs { get; set; }

    public virtual DbSet<VehiclePicture> VehiclePictures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-K56S2BSD\\SQLEXPRESS;Initial Catalog=SupreaLogistics;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaintenanceLog>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PK__Maintena__E60542B5904092F4");

            entity.Property(e => e.MaintenanceId).HasColumnName("MaintenanceID");
            entity.Property(e => e.Cost).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.MaintenanceType).HasMaxLength(100);
            entity.Property(e => e.ServiceProvider).HasMaxLength(200);
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.MaintenanceLogs)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_MaintenanceLogs_Vehicles");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54B21A713B5D");

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.BrandMake).HasMaxLength(100);
            entity.Property(e => e.ChassisNo).HasMaxLength(100);
            entity.Property(e => e.EngineNo).HasMaxLength(100);
            entity.Property(e => e.Insurance).HasMaxLength(100);
            entity.Property(e => e.InsuranceCoverage).HasMaxLength(200);
            entity.Property(e => e.InsuranceProvider).HasMaxLength(100);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Orcr).HasColumnName("ORCR");
            entity.Property(e => e.PlateNo).HasMaxLength(50);
            entity.Property(e => e.ProjectId)
                .HasMaxLength(100)
                .HasColumnName("ProjectID");
            entity.Property(e => e.SiteLocation).HasMaxLength(100);
            entity.Property(e => e.Supplier).HasMaxLength(200);
            entity.Property(e => e.UnitModelSeries).HasMaxLength(100);
            entity.Property(e => e.UnitType).HasMaxLength(100);
            entity.Property(e => e.VehicleStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<VehicleDocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__VehicleD__1ABEEF6FCB35433C");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.DocumentNumber).HasMaxLength(100);
            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FileType).HasMaxLength(50);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.UploadedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleDocuments)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehicleDocuments_Vehicles");
        });

        modelBuilder.Entity<VehicleLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__VehicleL__5E5499A84739A6A3");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DriverName).HasMaxLength(100);
            entity.Property(e => e.Purpose).HasMaxLength(255);
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleLogs)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehicleLogs_Vehicles");
        });

        modelBuilder.Entity<VehiclePicture>(entity =>
        {
            entity.HasKey(e => e.PictureId).HasName("PK__VehicleP__8C2866F86FB97AD6");

            entity.Property(e => e.PictureId).HasColumnName("PictureID");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FileType).HasMaxLength(50);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.UploadedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehiclePictures)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehiclePictures_Vehicles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
