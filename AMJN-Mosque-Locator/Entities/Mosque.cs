using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AMJN_Mosque_Locator.BaseEntities;
namespace AMJN_Mosque_Locator.Entities
{
    public class Mosque : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Address {get; set;}
        public string Region { get; set; }
        [Required]
        public string Circuit { get; set; }
        [Required]
        public string Jamaat { get; set; }
        public string State { get; set; }
        public string Contact { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public string GoogleMapsUrl => $"https://www.google.com/maps?q={Latitude},{Longitude}";
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } 
    }
}
