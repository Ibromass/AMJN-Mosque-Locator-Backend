namespace AMJN_Mosque_Locator.Implementations.Services
{
    public static class GeoService
    {
        private const double EarthRadiusKm = 6371.0;

        public static double CalculateDistance(
            double userLat, double userLng,
            double mosqueLat, double mosqueLng)
        {
            double dLat = ToRadians(mosqueLat - userLat);
            double dLon = ToRadians(mosqueLng - userLng);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(userLat)) * Math.Cos(ToRadians(mosqueLat)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        public static string GenerateDirectionsUrl(
            double userLat, double userLng,
            double mosqueLat, double mosqueLng)
        {
            return $"https://www.google.com/maps/dir/?api=1" +
                   $"&origin={userLat},{userLng}" +
                   $"&destination={mosqueLat},{mosqueLng}";
        }

        public static double ToRadians(double degrees)
            => degrees * (Math.PI / 180);
    }
}

