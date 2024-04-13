using InventoriaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
namespace InventoriaApi.Data;
public class InventoriaDBcontext : DbContext
{
    public InventoriaDBcontext(DbContextOptions<InventoriaDBcontext> options) : base(options)
    {
    }
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<AlertType> AlertTypes { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<DataCenter> DataCenters { get; set; }
    public DbSet<DataRack> DataRacks { get; set; }
    public DbSet<DataRacksChangeLog> DataRacksChangeLogs { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<EquipmentRackUnit> EquipmentRackUnits { get; set; }
    public DbSet<EnvironmentalReading> EnvironmentalReadings { get; set; }
    public DbSet<EnvironmentalSetting> EnvironmentalSettings { get; set; }
    public DbSet<RackAccessPermission> RackAccessPermissions { get; set; }
    public DbSet<RackUnit> RackUnits { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservedRackUnit> ReservedRackUnits { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ServerRoom> ServerRooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserChangeLog> UserChangeLogs { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Alert>(entity =>
        {
            entity.ToTable("Alert");
            entity.HasKey(alert => alert.AlertID);
            entity.HasOne(alert => alert.AlertType).WithMany(at => at.Alerts).HasForeignKey(alert => alert.AlertTypeID);
            entity.HasOne(alert => alert.EnvironmentalReading).WithMany().HasForeignKey(alert => alert.EnvironmentalReadingID);
        });

        modelBuilder.Entity<AlertType>(entity =>
        {
            entity.ToTable("AlertType");
            entity.HasKey(at => at.AlertTypeID);
            entity.Property(at => at.TypeName).IsRequired().HasMaxLength(255);
            entity.Property(at => at.Description).HasColumnType("text");
            entity.HasMany(at => at.Alerts).WithOne(alert => alert.AlertType).HasForeignKey(alert => alert.AlertTypeID);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");
            entity.HasKey(com => com.CompanyID);
            entity.Property(com => com.Name).IsRequired().HasMaxLength(255);
            entity.Property(com => com.Description).HasColumnType("text");
            entity.HasMany(com => com.Users).WithOne(user => user.Company).HasForeignKey(user => user.CompanyID);
            entity.HasMany(com => com.DataCenters).WithOne(data => data.Company).HasForeignKey(data => data.CompanyID);
        });

        modelBuilder.Entity<DataCenter>(entity =>
        {
            entity.ToTable("DataCenter");
            entity.HasKey(data => data.DataCenterID);
            entity.Property(data => data.Address).IsRequired().HasMaxLength(255);
            entity.Property(data => data.Description).IsRequired().HasColumnType("text");
            entity.HasOne(data => data.Company).WithMany(com => com.DataCenters).HasForeignKey(data => data.CompanyID);
            entity.HasMany(data => data.ServerRooms).WithOne(sr => sr.DataCenter).HasForeignKey(sr => sr.DataCenterID);
        });

        modelBuilder.Entity<DataRack>(entity =>
        {
            entity.ToTable("DataRack");
            entity.HasKey(dr => dr.DataRackID);
            entity.Property(dr => dr.RackPlacement).IsRequired();
            entity.Property(dr => dr.TotalUnits).IsRequired();
            entity.Property(dr => dr.AvailableUnits).IsRequired();
            entity.Property(dr => dr.Status).IsRequired();
            entity.Property(dr => dr.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(dr => dr.ServerRoom).WithMany(sr => sr.DataRacks).HasForeignKey(dr => dr.ServerRoomID);
            entity.HasMany(dr => dr.RackUnits).WithOne(ru => ru.DataRack).HasForeignKey(ru => ru.DataRackID);
            entity.HasMany(dr => dr.DataRacksChangeLogs).WithOne(drc => drc.DataRack).HasForeignKey(drc => drc.DataRackID);
        });

        modelBuilder.Entity<DataRacksChangeLog>(entity =>
        {
            entity.ToTable("DataRacksChangeLog");
            entity.HasKey(drcl => drcl.DataRacksChangeLogID);
            entity.Property(drcl => drcl.ChangeType).IsRequired().HasMaxLength(255);
            entity.Property(drcl => drcl.ChangeTimestamp).IsRequired();
            entity.Property(drcl => drcl.ChangeDescription).HasColumnType("text");
            entity.HasOne(drcl => drcl.DataRack).WithMany(dr => dr.DataRacksChangeLogs).HasForeignKey(drcl => drcl.DataRackID);
            entity.HasOne(drcl => drcl.ChangedByUser).WithMany().HasForeignKey(drcl => drcl.ChangedByUserID);
        });

        modelBuilder.Entity<EnvironmentalReading>(entity =>
        {
            entity.ToTable("EnvironmentalReading");
            entity.HasKey(er => er.EnvironmentalReadingID);
            entity.Property(er => er.Temperature).IsRequired().HasColumnType("float");
            entity.Property(er => er.Humidity).IsRequired().HasColumnType("float");
            entity.Property(er => er.ReadingTimestamp).IsRequired().HasColumnType("datetime");
        });

        modelBuilder.Entity<EnvironmentalSetting>(entity =>
        {
            entity.ToTable("EnvironmentalSetting");
            entity.HasKey(es => es.EnvironmentalSettingID);
            entity.Property(es => es.TemperatureUpperLimit).IsRequired();
            entity.Property(es => es.TemperatureLowerLimit).IsRequired();
            entity.Property(es => es.HumidityUpperLimit).IsRequired();
            entity.Property(es => es.HumidityLowerLimit).IsRequired();
            entity.Property(es => es.LatestChange).IsRequired();
            entity.HasOne(es => es.ServerRoom).WithOne(sr => sr.EnvironmentalSetting).
                HasForeignKey<EnvironmentalSetting>(es => es.ServerRoomID);
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.ToTable("Equipment");
            entity.HasKey(e => e.EquipmentID);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Model).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Type).IsRequired().HasMaxLength(255);
            entity.HasMany(e => e.EquipmentRackUnits).WithOne(eru => eru.Equipment).HasForeignKey(eru => eru.EquipmentID);
        });

        modelBuilder.Entity<EquipmentRackUnit>(entity =>
        {
            entity.ToTable("EquipmentRackUnit");
            entity.HasKey(eru => eru.EquipmentRackUnitID);
            entity.HasOne(eru => eru.Equipment).WithMany(e => e.EquipmentRackUnits).HasForeignKey(eru => eru.EquipmentID);
            entity.HasOne(eru => eru.RackUnit).WithMany(ru => ru.EquipmentRackUnits).HasForeignKey(eru => eru.RackUnitID);
        });

        modelBuilder.Entity<RackAccessPermission>(entity =>
        {
            entity.ToTable("RackAccessPermission");
            entity.HasKey(rap => rap.RackAccessPermissionID);
            entity.HasOne(rap => rap.DataRack).WithMany(dr => dr.RackAccessPermissions).HasForeignKey(rap => rap.DataRackID);
            entity.HasOne(rap => rap.Role).WithMany(r => r.RackAccessPermissions).HasForeignKey(rap => rap.RoleID);
        });

        modelBuilder.Entity<RackUnit>(entity =>
        {
            entity.ToTable("RackUnit");
            entity.HasKey(ru => ru.RackUnitID);
            entity.Property(ru => ru.UnitNumber).IsRequired();
            entity.HasOne(ru => ru.DataRack).WithMany(dr => dr.RackUnits).HasForeignKey(ru => ru.DataRackID);
            entity.HasMany(ru => ru.ReservedRackUnits).WithOne(rru => rru.RackUnit).HasForeignKey(rru => rru.RackUnitID);
            entity.HasMany(ru => ru.EquipmentRackUnits).WithOne(eru => eru.RackUnit).HasForeignKey(eru => eru.RackUnitID);
        });


        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.ToTable("Reservation");
            entity.HasKey(r => r.ReservationID);
            entity.Property(r => r.StartDate).IsRequired();
            entity.Property(r => r.EndDate).IsRequired();
            entity.Property(r => r.Background).HasColumnType("text");
            entity.HasOne(r => r.User).WithMany(u => u.Reservations).HasForeignKey(r => r.UserID);
            entity.HasMany(r => r.ReservedRackUnits).WithOne(rru => rru.Reservation).HasForeignKey(rru => rru.ReservationID);
        });


        modelBuilder.Entity<ReservedRackUnit>(entity =>
        {
            entity.ToTable("ReservedRackUnit");
            entity.HasKey(rru => rru.ReservedRackUnitID);
            entity.HasOne(rru => rru.Reservation).WithMany(r => r.ReservedRackUnits).HasForeignKey(rru => rru.ReservationID);
            entity.HasOne(rru => rru.RackUnit).WithMany().HasForeignKey(rru => rru.RackUnitID);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
            entity.HasKey(r => r.RoleID);
            entity.HasIndex(r => r.RoleName).IsUnique();
            entity.Property(r => r.RoleName).IsRequired().HasMaxLength(255);
            entity.HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleID);
            entity.HasMany(r => r.RackAccessPermissions).WithOne(rap => rap.Role).HasForeignKey(rap => rap.RoleID);

        });

        modelBuilder.Entity<ServerRoom>(entity =>
        {
            entity.ToTable("ServerRoom");
            entity.HasKey(sr => sr.ServerRoomID);
            entity.Property(sr => sr.ServerRoomName).IsRequired().HasMaxLength(255);
            entity.Property(sr => sr.RackCapacity).IsRequired();
            entity.Property(sr => sr.StartupDate).IsRequired();
            entity.HasOne(sr => sr.DataCenter).WithMany(dc => dc.ServerRooms).HasForeignKey(sr => sr.DataCenterID);
            entity.HasMany(sr => sr.DataRacks).WithOne(dr => dr.ServerRoom).HasForeignKey(dr => dr.ServerRoomID);
            entity.HasOne(sr => sr.EnvironmentalSetting).WithOne(es => es.ServerRoom).HasForeignKey<EnvironmentalSetting>(es => es.ServerRoomID);
        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(u => u.UserID);
            entity.Property(u => u.Displayname).IsRequired().HasMaxLength(255);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.PasswordSalt).IsRequired();
            entity.Property(u => u.StudieEmail).IsRequired().HasMaxLength(255);
            entity.HasIndex(u => u.StudieEmail).IsUnique();
            entity.Property(u => u.CreationDate).IsRequired();
            entity.Property(u => u.LastLoginDate).IsRequired();
            entity.HasOne(u => u.Company).WithMany(c => c.Users).HasForeignKey(u => u.CompanyID);
            entity.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserID);
            entity.HasMany(u => u.Reservations).WithOne(r => r.User).HasForeignKey(r => r.UserID);
            entity.HasMany(u => u.DataRacksChangeLogs).WithOne(drc => drc.ChangedByUser).HasForeignKey(drc => drc.ChangedByUserID);
            entity.HasMany(u => u.UserChangeLogs).WithOne(ucl => ucl.User).HasForeignKey(ucl => ucl.UserID);
        });

        modelBuilder.Entity<UserChangeLog>(entity =>
        {
            entity.ToTable("UserChangeLog");
            entity.HasKey(ucl => ucl.UserChangeLogID);

            entity.Property(ucl => ucl.ChangeType).IsRequired().HasMaxLength(255);
            entity.Property(ucl => ucl.ChangeTimestamp).IsRequired();
            entity.Property(ucl => ucl.ChangeDescription).HasColumnType("text");
            entity.HasOne(ucl => ucl.User).WithMany(u => u.UserChangeLogs).HasForeignKey(ucl => ucl.UserID);
            entity.HasOne(ucl => ucl.ChangedByUser).WithMany().HasForeignKey(ucl => ucl.ChangedByUserID);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");
            entity.HasKey(ur => ur.UserRoleID);
            entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserID);
            entity.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleID);
        });
    }
}