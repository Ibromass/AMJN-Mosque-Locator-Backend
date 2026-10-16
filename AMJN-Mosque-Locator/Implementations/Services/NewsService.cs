using AMJN_Mosque_Locator.DTOs.NewsDtos;
using AMJN_Mosque_Locator.Entities;
using AMJN_Mosque_Locator.Interfaces.Repositories;
using AMJN_Mosque_Locator.Interfaces.Services;

namespace AMJN_Mosque_Locator.Implementations.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _repository;

    public NewsService(INewsRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<NewsItemDto>> GetAllAsync() =>
        (await _repository.GetAllAsync()).Select(ToDto).ToList();

    public async Task<NewsItemDto?> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : ToDto(item);
    }

    public async Task<NewsItemDto> CreateAsync(UpsertNewsItemRequest request)
    {
        var now = DateTime.UtcNow;
        var item = ToEntity(request);
        item.Id = Guid.NewGuid();
        item.DateCreated = now;
        item.DateUpdated = now;
        return ToDto(await _repository.CreateAsync(item));
    }

    public async Task<NewsItemDto?> UpdateAsync(Guid id, UpsertNewsItemRequest request)
    {
        var updated = await _repository.UpdateAsync(id, ToEntity(request));
        return updated is null ? null : ToDto(updated);
    }

    public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);

    private static NewsItem ToEntity(UpsertNewsItemRequest request) => new()
    {
        Category = request.Category.Trim().ToLowerInvariant() switch
        {
            "friday sermon" => "Friday Sermon",
            "nigeria" => "Nigeria",
            _ => "World",
        },
        Title = request.Title.Trim(),
        DisplayDate = request.DisplayDate.Trim(),
        Summary = request.Summary.Trim(),
        Url = request.Url.Trim(),
        Tag = string.IsNullOrWhiteSpace(request.Tag) ? null : request.Tag.Trim(),
        WatchUrl = string.IsNullOrWhiteSpace(request.WatchUrl) ? null : request.WatchUrl.Trim(),
    };

    private static NewsItemDto ToDto(NewsItem item) => new()
    {
        Id = item.Id,
        Category = item.Category,
        Title = item.Title,
        DisplayDate = item.DisplayDate,
        Summary = item.Summary,
        Url = item.Url,
        Tag = item.Tag,
        WatchUrl = item.WatchUrl,
        DateCreated = item.DateCreated,
        DateUpdated = item.DateUpdated,
    };
}
