using AMJN_Mosque_Locator.DTOs;
using AMJN_Mosque_Locator.Entities;
namespace AMJN_Mosque_Locator.Interfaces.Repositories
{
    public interface IMosqueRepository
    {
        Task<IEnumerable<Mosque>> GetAllAsync();
        Task<Mosque> GetByIdAsync(Guid id);
        Task<IEnumerable<(Mosque Mosque, double Distance)>> GetNearbyWithDistanceAsync(NearbySearchDto query);
        Task<IEnumerable<Mosque>> SearchAsync(string? state,string? region,string? circuit,string? jamaat);
        Task<Mosque> CreateAsync(Mosque model);
        Task<Mosque> UpdateAsync(Guid id, Mosque updated);
        Task<bool> DeleteAsync(Guid id);   
    }
}
