using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using AMJN_Mosque_Locator.BaseEntities;     

namespace AMJN_Mosque_Locator.DTOs.MosqueDtos
{
    public class MosqueDto
    {
        internal string DirectionsUrl;

        public Guid Id { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Region { get; set; }
        public string? Circuit { get; set; }
        public string? Jamaat { get; set; }
        public string? Contact { get; set; }
        public string? ImageUrl { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public double DistanceKm { get; internal set; }
    }
}
