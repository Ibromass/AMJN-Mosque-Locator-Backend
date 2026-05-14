using AMJN_Mosque_Locator.Entities;

namespace AMJN_Mosque_Locator.Interfaces.Repositories
{
    public interface IAdminauthRepository
    {
        Task<Admin> GetByUsernameAsync(string username);
        Task UpdateLastLoginAsync(Admin admin);
    }
}
