using System.ComponentModel.DataAnnotations;

namespace AMJN_Mosque_Locator.DTOs
{
    public class NearbySearchDto
    {
     [Required]
     public double Latitude { get; set;}
     [Required]
     public double Longitude { get; set;}
     public double RadiusKm { get; set; } = 15; //15Km if user does not specify 
    }
}
