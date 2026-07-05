using AMJN_Mosque_Locator.DTOs.LoginDtos;
using AMJN_Mosque_Locator.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AMJN_Mosque_Locator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminauthController : ControllerBase
    {

        private readonly IAdminauthService _authService;

        public AdminauthController(IAdminauthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestModel dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized(new { message = "Invalid username or password."});

            return Ok(result);
        }
    }
}
