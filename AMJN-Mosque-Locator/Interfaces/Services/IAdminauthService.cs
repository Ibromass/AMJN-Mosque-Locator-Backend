using AMJN_Mosque_Locator.DTOs.LoginDtos;
using AMJN_Mosque_Locator.Entities;
using AMJN_Mosque_Locator.DTOs.LoginDtos;

namespace AMJN_Mosque_Locator.Interfaces.Services
{
    public interface IAdminauthService
    {
        Task<LoginResponseDto> LoginAsync (LoginRequestModel dto);
    }
}
