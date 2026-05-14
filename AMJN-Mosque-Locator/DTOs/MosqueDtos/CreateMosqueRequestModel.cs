using System.ComponentModel.DataAnnotations;

namespace AMJN_Mosque_Locator.DTOs.MosqueDtos
{
    public class CreateMosqueRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string? Address { get; set; }
        [Required]
        public string? Region { get; set; }
        public string Circuit { get; set; }
        [Required]
        public string? Jamaat { get; set; }
        public string State { get; set; }
        public string? Contact { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public string GoogleMapsUrl => $"https://www.google.com/maps?q={Latitude},{Longitude}";
        public List<string> ImageUrl { get; set; } = new();
    }
}
