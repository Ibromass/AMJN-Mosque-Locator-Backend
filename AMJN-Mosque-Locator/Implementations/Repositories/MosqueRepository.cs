using AMJN_Mosque_Locator.AppDbContext;
using AMJN_Mosque_Locator.DTOs;
using AMJN_Mosque_Locator.Entities;
using AMJN_Mosque_Locator.Implementations;
using AMJN_Mosque_Locator.Implementations.Services;
using AMJN_Mosque_Locator.Interfaces.Repositories;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace AMJN_Mosque_Locator.Implementations.Repositories
{
    public class MosqueRepository : IMosqueRepository
    {
        private readonly AMJNDbContext _context;
        public MosqueRepository(AMJNDbContext context)
        {
            _context = context;
        }


        public async Task<Mosque> CreateAsync(Mosque mosque)
        {
            mosque.IsActive = true;
            _context.Mosques.Add(mosque);
            await _context.SaveChangesAsync();
            return mosque;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var mosque = await _context.Mosques.FindAsync(id);
            if (mosque == null) return false;

            mosque.IsActive = false;
            mosque.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Mosque>> GetAllAsync()
        {
            return await _context.Mosques
                .AsNoTracking()
                .Where(m => m.IsActive)
                .ToListAsync();
        }

        public async Task<Mosque> GetByIdAsync(Guid id)
        {
           return await _context.Mosques
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<(Mosque Mosque, double Distance)>> GetNearbyWithDistanceAsync(NearbySearchDto query)
        {
            // Bounding box filter (fast DB query)
            var latDelta = query.RadiusKm / 111.0;
            var lngDelta = query.RadiusKm / (111.0 * Math.Cos(GeoService.ToRadians(query.Latitude)));

            var candidates = await _context.Mosques
                .AsNoTracking()
                .Where(m => m.IsActive)
                .Where(m => m.Latitude >= query.Latitude - latDelta && m.Latitude <= query.Latitude + latDelta)
                .Where(m => m.Longitude >= query.Longitude - lngDelta && m.Longitude <= query.Longitude + lngDelta)
                //.Where(m => m.Distance <= query.RadiusKm)

                .ToListAsync();

            // Calculate distance once, return with mosque
            return candidates
                .Select(m => new
                {
                    Mosque = m,
                    Distance = GeoService.CalculateDistance(
                        query.Latitude, query.Longitude,
                        m.Latitude, m.Longitude)
                })
                .Where(x => x.Distance <= query.RadiusKm)
                .OrderBy(x => x.Distance)
                .Select(x => (x.Mosque, x.Distance))
                .ToList();
        }

        public async Task<IEnumerable<Mosque>> SearchAsync(string? state, string? region, string? circuit, string? jamaat)
        {
            var query = _context.Mosques
                .AsNoTracking()
                .Where(m => m.IsActive)  // Only active mosques
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(state))
                query = query.Where(m => m.State.ToLower().Contains(state.ToLower()));

            if (!string.IsNullOrWhiteSpace(region))
                query = query.Where(m => m.Region.ToLower().Contains(region.ToLower()));

            if (!string.IsNullOrWhiteSpace(circuit))
                query = query.Where(m => m.Circuit.ToLower().Contains(circuit.ToLower()));

            if (!string.IsNullOrWhiteSpace(jamaat))
                query = query.Where(m => m.Jamaat.ToLower().Contains(jamaat.ToLower()));

            // ✅ FIX: Order by name for consistent results
            return await query
                .OrderBy(m => m.State)
                .ThenBy(m => m.Region)
                .ThenBy(m => m.Circuit)
                .ThenBy(m => m.Jamaat)
                .ToListAsync();
        }


        public async Task<Mosque> UpdateAsync(Guid id, Mosque updated)
        {
            var existing = await _context.Mosques.FindAsync(id);
            if (existing == null) return null;

            // Only update if the incoming value is NOT null or empty
            //if (!string.IsNullOrWhiteSpace(updated.Name)) existing.Name = updated.Name;
            //if (!string.IsNullOrWhiteSpace(updated.Address)) existing.Address = updated.Address;
            //if (!string.IsNullOrWhiteSpace(updated.State)) existing.State = updated.State;
            //if (!string.IsNullOrWhiteSpace(updated.Region)) existing.Region = updated.Region;
            //if (!string.IsNullOrWhiteSpace(updated.Circuit)) existing.Circuit = updated.Circuit;
            //if (!string.IsNullOrWhiteSpace(updated.Jamaat)) existing.Jamaat = updated.Jamaat;
            //if (!string.IsNullOrWhiteSpace(updated.Contact)) existing.Contact = updated.Contact;
            //if (!string.IsNullOrWhiteSpace(updated.ImageUrl)) existing.ImageUrl = updated.ImageUrl;

            // For coordinates, check if they are non-zero (assuming 0,0 isn't a valid mosque location)
            //if (updated.Latitude != 0) existing.Latitude = updated.Latitude;
            //if (updated.Longitude != 0) existing.Longitude = updated.Longitude;

            //existing.DateUpdated = DateTime.UtcNow;

            //await _context.SaveChangesAsync();
            //return existing;

            existing.Name = updated.Name;
            existing.Address = updated.Address;
            existing.State = updated.State;
            existing.Region = updated.Region;
            existing.Circuit = updated.Circuit;
            existing.Jamaat = updated.Jamaat;
            existing.Contact = updated.Contact;
            existing.ImageUrl = updated.ImageUrl;
            existing.Latitude = updated.Latitude;
            existing.Longitude = updated.Longitude;

            existing.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

    }

}
