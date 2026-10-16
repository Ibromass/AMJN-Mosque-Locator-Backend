using AMJN_Mosque_Locator.Entities;

namespace AMJN_Mosque_Locator.Interfaces.Repositories;

public interface INewsRepository
{
    Task<IReadOnlyList<NewsItem>> GetAllAsync();
    Task<NewsItem?> GetByIdAsync(Guid id);
    Task<NewsItem> CreateAsync(NewsItem item);
    Task<NewsItem?> UpdateAsync(Guid id, NewsItem item);
    Task<bool> DeleteAsync(Guid id);
}
