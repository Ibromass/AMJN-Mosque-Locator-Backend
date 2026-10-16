using System.ComponentModel.DataAnnotations;

namespace AMJN_Mosque_Locator.DTOs.NewsDtos;

public class UpsertNewsItemRequest
{
    [Required, MaxLength(40)]
    public string Category { get; set; } = string.Empty;

    [Required, MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(80)]
    public string DisplayDate { get; set; } = string.Empty;

    [Required, MaxLength(3000)]
    public string Summary { get; set; } = string.Empty;

    [Required, Url, MaxLength(1000)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Tag { get; set; }

    [Url, MaxLength(1000)]
    public string? WatchUrl { get; set; }
}
