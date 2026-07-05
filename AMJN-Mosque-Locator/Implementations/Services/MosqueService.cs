using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using AMJN_Mosque_Locator.DTOs;
using AMJN_Mosque_Locator.DTOs.MosqueDtos;
using AMJN_Mosque_Locator.Entities;
using AMJN_Mosque_Locator.Interfaces.Repositories;
using AMJN_Mosque_Locator.Interfaces.Services;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AMJN_Mosque_Locator.Implementations.Services
{
    public class MosqueService : IMosqueService
    {
        private readonly IMosqueRepository _repository;

        public MosqueService(IMosqueRepository repository)
        {
            _repository = repository;
        }

        public async Task<MosqueDto> CreateAsync(CreateMosqueRequestModel dto)
        {
            var mosque = new Mosque
            {
                Name = dto.Name,
                Address  = dto.Address = string.Empty,
                State = dto.State = string.Empty,
                Region = dto.Region = string.Empty,
                Circuit = dto.Circuit,
                Jamaat = dto.Jamaat = string.Empty,
                Contact = dto.Contact = string.Empty,
                ImageUrl = dto.ImageUrl,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
            };

            var created = await _repository.CreateAsync(mosque);
            return MapToDto(created);
        }

        public async Task<IEnumerable<MosqueDto>> GetAllAsync()
        {
            var mosques = await _repository.GetAllAsync();
            return mosques.Select(MapToDto);
        }

        public async Task<MosqueDto> GetByIdAsync(Guid id)
        {

            var mosque = await _repository.GetByIdAsync(id);

            if (mosque == null) return null;

            return MapToDto(mosque);
        }

        public async Task<IEnumerable<MosqueDto>> GetNearbySearchAsync(NearbySearchDto query)
        {
            
            var mosquesWithDistance = await _repository.GetNearbyWithDistanceAsync(query);

            return mosquesWithDistance.Select(x =>
            {
                var dto = MapToDto(x.Mosque);

                
                dto.DistanceKm = Math.Round(x.Distance, 4);
                dto.DistanceKm = x.Distance;
                
                dto.DirectionsUrl = GeoService.GenerateDirectionsUrl(
                    query.Latitude, query.Longitude,
                    x.Mosque.Latitude, x.Mosque.Longitude);

                return dto;
            })
            .OrderBy(m => m.DistanceKm)
            .ToList();
        }

        public async Task<IEnumerable<MosqueDto>> SearchAsync(string? state,string? region,string? circuit, string? jamaat)
        {
            
            var mosques = await _repository.SearchAsync(state, region, circuit, jamaat);

            return mosques
                .Select(m => MapToDto(m))
                .OrderBy(m => m.State)
                .ThenBy(m => m.Region)
                .ThenBy(m => m.Circuit)
                .ThenBy(m => m.Jamaat);
        }

        public async Task<MosqueDto> UpdateAsync(Guid id, UpdateMosqueRequestModel dto)
        {
            var updated = new Mosque
            {
                Id = id, 
                Name = dto.Name,
                Address = dto.Address,
                State = dto.State,
                Region = dto.Region,
                Circuit = dto.Circuit,
                Jamaat = dto.Jamaat,
                Contact = dto.Contact,
                ImageUrl = dto.ImageUrl,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            var result = await _repository.UpdateAsync(id, updated);

            return result == null ? null : MapToDto(result);
            
        }

        private MosqueDto MapToDto(Mosque mosque)
        {
            return new MosqueDto
            {
                Id = mosque.Id,
                Name = mosque.Name,
                Address = mosque.Address,
                State = mosque.State,
                Region = mosque.Region,
                Circuit = mosque.Circuit,
                Jamaat = mosque.Jamaat,
                Contact = mosque.Contact,
                ImageUrl = mosque.ImageUrl,
                Latitude = mosque.Latitude,
                Longitude = mosque.Longitude,
                DateCreated = mosque.DateCreated,
                DateUpdated = mosque.DateUpdated
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var mosque = await _repository.GetByIdAsync(id);
            if (mosque == null) return false;

            return await _repository.DeleteAsync(id);
        }
    }
}
