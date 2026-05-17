namespace AMJN_Mosque_Locator.DTOs.MosqueDtos
{
    public class UpdateMosqueRequestModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string Circuit { get; set; }
        public string Jamaat { get; set; }
        public string State { get; set; }
        public string Contact { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? ImageUrl { get; set; }
    }
}
