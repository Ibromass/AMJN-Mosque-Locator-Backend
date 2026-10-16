using System.ComponentModel.DataAnnotations;
using AMJN_Mosque_Locator.BaseEntities;

namespace AMJN_Mosque_Locator.Entities;

public class NewsItem : BaseEntity
{
    [Required, MaxLength(40)]
    public string Category { get; set; } = string.Empty;

    [Required, MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(80)]
    public string DisplayDate { get; set; } = string.Empty;

    [Required, MaxLength(3000)]
    public string Summary { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Tag { get; set; }

    [MaxLength(1000)]
    public string? WatchUrl { get; set; }
}
