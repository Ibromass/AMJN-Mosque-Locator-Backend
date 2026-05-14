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
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("AMJNAdmin@123"),
                    IsActive = true,
                    LastLoginAt = null,
                    DateCreated = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), // ← fixed date
                    DateUpdated = DateTime.UtcNow
                }
            );
        
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mosque>()
                .Property(m => m.ImageUrl)
                .HasColumnType("json");

    }

}
    
}
