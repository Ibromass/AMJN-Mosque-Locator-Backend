using Microsoft.AspNetCore.Identity;
using AMJN_Mosque_Locator.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace AMJN_Mosque_Locator.Entities
{
    public class Admin : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public required string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }

    }
}
