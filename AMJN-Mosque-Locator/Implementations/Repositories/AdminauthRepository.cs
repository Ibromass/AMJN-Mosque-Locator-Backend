using AMJN_Mosque_Locator.AppDbContext;
using AMJN_Mosque_Locator.Entities;
using Microsoft.EntityFrameworkCore;
using AMJN_Mosque_Locator.Interfaces.Repositories;

namespace AMJN_Mosque_Locator.Implementations.Repositories
{
    public class AdminauthRepository : IAdminauthRepository
    {
        private readonly AMJNDbContext _context;
        public AdminauthRepository(AMJNDbContext context)
        {
            _context = context;
        }
        public async Task<Admin> GetByUsernameAsync(string username)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == username && a.IsActive == true);
        }
        public async Task UpdateLastLoginAsync(Admin admin)
        {
            admin.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
