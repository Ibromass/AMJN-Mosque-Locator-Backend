using AMJN_Mosque_Locator.DTOs;
using AMJN_Mosque_Locator.DTOs.MosqueDtos;

namespace AMJN_Mosque_Locator.Interfaces.Services
{
    public interface IMosqueService
    {
        Task<IEnumerable<MosqueDto>> GetAllAsync();
        Task<MosqueDto> GetByIdAsync(Guid id);

       
        Task<IEnumerable<MosqueDto>> SearchAsync(string? state,string? region,string? circuit,string? jamaat);

        
        Task<IEnumerable<MosqueDto>> GetNearbySearchAsync(NearbySearchDto query);

        Task<MosqueDto> CreateAsync(CreateMosqueRequestModel dto);
        Task<MosqueDto> UpdateAsync(Guid id, UpdateMosqueRequestModel dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
