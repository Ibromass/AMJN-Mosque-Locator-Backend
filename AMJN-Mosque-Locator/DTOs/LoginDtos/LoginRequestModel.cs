using AMJN_Mosque_Locator.DTOs;
using AMJN_Mosque_Locator.Entities;
namespace AMJN_Mosque_Locator.DTOs.LoginDtos
{
public class LoginRequestModel
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
}