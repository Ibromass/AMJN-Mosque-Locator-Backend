using AMJN_Mosque_Locator.DTOs;
using AMJN_Mosque_Locator.Entities;
namespace AMJN_Mosque_Locator.DTOs.LoginDtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}