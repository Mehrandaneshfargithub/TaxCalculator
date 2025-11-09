using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EntityModels;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CityHoliday> CityHolidays { get; set; }

    public virtual DbSet<CityTaxRule> CityTaxRules { get; set; }

    public virtual DbSet<CityVehicleTypeRule> CityVehicleTypeRules { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehiclePassing> VehiclePassings { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=DESKTOP-PIVSERB;initial catalog=TaxCalculator;persist security info=True;user id=sa;password=1990301223;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC0796FDED0A");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CityHoliday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CityHoli__3214EC07E522F3BB");

            entity.HasOne(d => d.City).WithMany(p => p.CityHolidays)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CityHolid__CityI__2F10007B");
        });

        modelBuilder.Entity<CityTaxRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CityTaxR__3214EC07AA625DCC");

            entity.HasOne(d => d.City).WithMany(p => p.CityTaxRules)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CityTaxRu__CityI__2C3393D0");
        });

        modelBuilder.Entity<CityVehicleTypeRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CityVehi__3214EC073D75F951");

            entity.HasOne(d => d.City).WithMany(p => p.CityVehicleTypeRules)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CityVehic__CityI__286302EC");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.CityVehicleTypeRules)
                .HasForeignKey(d => d.VehicleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CityVehic__Vehic__29572725");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicles__3214EC07A1266C72");

            entity.Property(e => e.LicensePlate).HasMaxLength(20);

            entity.HasOne(d => d.VehicleType).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.VehicleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vehicles__Vehicl__31EC6D26");
        });

        modelBuilder.Entity<VehiclePassing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleP__3214EC070A15E6C1");

            entity.Property(e => e.PassingDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.VehiclePassings)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__VehiclePa__CityI__35BCFE0A");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehiclePassings)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__VehiclePa__Vehic__34C8D9D1");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VehicleT__3214EC07DB2A862C");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
