using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Models;
using System;

namespace SUPREA_LOGISTICS.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
       : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<VehicleLog> vehicleLogs { get; set; }
    }
}
