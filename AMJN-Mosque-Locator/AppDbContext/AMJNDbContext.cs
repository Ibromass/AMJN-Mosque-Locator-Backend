using System.Data;
using AMJN_Mosque_Locator.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
namespace AMJN_Mosque_Locator.AppDbContext
{
    public class AMJNDbContext : DbContext
    {
        public AMJNDbContext(DbContextOptions<AMJNDbContext> options) : base(options)
        {

        }
         
         public DbSet<Mosque> Mosques { get; set; }
         public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminId = new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890");

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = adminId,
                    Username = "Admin",
                    Email = "admin@amjn.org",
                    PasswordHash = "$2a$11$wll3su1NBcToAtsSLYHWfeK4DVnb8m.sf9.tNVt4YoBhwASIJegQq",
                    IsActive = true,
                    LastLoginAt = null,
                    DateCreated = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    DateUpdated = new DateTime(2026, 5, 14, 14, 34, 5, 193, DateTimeKind.Utc).AddTicks(3980)
                }
            );
        
            base.OnModelCreating(modelBuilder);

        }

    }
    
}
