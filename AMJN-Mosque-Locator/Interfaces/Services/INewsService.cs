using AMJN_Mosque_Locator.DTOs.NewsDtos;

namespace AMJN_Mosque_Locator.Interfaces.Services;

public interface INewsService
{
    Task<IReadOnlyList<NewsItemDto>> GetAllAsync();
    Task<NewsItemDto?> GetByIdAsync(Guid id);
    Task<NewsItemDto> CreateAsync(UpsertNewsItemRequest request);
    Task<NewsItemDto?> UpdateAsync(Guid id, UpsertNewsItemRequest request);
    Task<bool> DeleteAsync(Guid id);
}
