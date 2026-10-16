using AMJN_Mosque_Locator.AppDbContext;
using AMJN_Mosque_Locator.Entities;
using AMJN_Mosque_Locator.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AMJN_Mosque_Locator.Implementations.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly AMJNDbContext _context;

    public NewsRepository(AMJNDbContext context) => _context = context;

    public async Task<IReadOnlyList<NewsItem>> GetAllAsync() =>
        await _context.NewsItems.AsNoTracking()
            .OrderByDescending(item => item.DateCreated)
            .ToListAsync();

    public Task<NewsItem?> GetByIdAsync(Guid id) =>
        _context.NewsItems.FirstOrDefaultAsync(item => item.Id == id);

    public async Task<NewsItem> CreateAsync(NewsItem item)
    {
        _context.NewsItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<NewsItem?> UpdateAsync(Guid id, NewsItem updated)
    {
        var item = await GetByIdAsync(id);
        if (item is null) return null;

        item.Category = updated.Category;
        item.Title = updated.Title;
        item.DisplayDate = updated.DisplayDate;
        item.Summary = updated.Summary;
        item.Url = updated.Url;
        item.Tag = updated.Tag;
        item.WatchUrl = updated.WatchUrl;
        item.DateUpdated = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await GetByIdAsync(id);
        if (item is null) return false;

        _context.NewsItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
