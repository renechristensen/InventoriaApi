using InventoriaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<DataCenter> DataCenters { get; set; }
    public DbSet<ServerRoom> ServerRooms { get; set; }
    public DbSet<DataRack> DataRacks { get; set; }
    public DbSet<RackUnit> RackUnits { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservedRackUnit> ReservedRackUnits { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<EquipmentRackUnit> EquipmentRackUnits { get; set; }
    public DbSet<EnvironmentalReading> EnvironmentalReadings { get; set; }
    public DbSet<EnvironmentalSetting> EnvironmentalSettings { get; set; }
    public DbSet<AlertType> AlertTypes { get; set; }
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<RackAccessPermission> RackAccessPermissions { get; set; }
    public DbSet<UserChangeLog> UserChangeLogs { get; set; }
    public DbSet<DataRacksChangeLog> DataRacksChangeLogs { get; set; }
}
