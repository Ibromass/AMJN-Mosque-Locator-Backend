using AMJN_Mosque_Locator.DTOs.NewsDtos;
using AMJN_Mosque_Locator.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMJN_Mosque_Locator.Controllers;

[ApiController]
[Route("api/news")]
public class NewsController : ControllerBase
{
    private static readonly HashSet<string> AllowedCategories = new(StringComparer.OrdinalIgnoreCase)
    {
        "Friday Sermon",
        "Nigeria",
        "World",
    };

    private readonly INewsService _service;

    public NewsController(INewsService service) => _service = service;

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<NewsItemDto>>> GetAll() =>
        Ok(await _service.GetAllAsync());

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NewsItemDto>> GetById(Guid id) =>
        (await _service.GetByIdAsync(id)) is { } item ? Ok(item) : NotFound();

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<NewsItemDto>> Create([FromBody] UpsertNewsItemRequest request)
    {
        if (!IsAllowed(request.Category))
            return BadRequest(new { message = "Category must be Friday Sermon, Nigeria, or World." });

        var item = await _service.CreateAsync(request);
        return Created($"/api/news/{item.Id}", item);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<NewsItemDto>> Update(Guid id, [FromBody] UpsertNewsItemRequest request)
    {
        if (!IsAllowed(request.Category))
            return BadRequest(new { message = "Category must be Friday Sermon, Nigeria, or World." });

        var item = await _service.UpdateAsync(id, request);
        return item is null ? NotFound() : Ok(item);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) =>
        await _service.DeleteAsync(id) ? NoContent() : NotFound();

    private static bool IsAllowed(string? category) =>
        category is not null && AllowedCategories.Contains(category.Trim());
}
