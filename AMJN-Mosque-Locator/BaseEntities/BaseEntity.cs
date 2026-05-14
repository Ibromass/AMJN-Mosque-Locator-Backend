using Microsoft.EntityFrameworkCore;
namespace AMJN_Mosque_Locator.BaseEntities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

    }
}
