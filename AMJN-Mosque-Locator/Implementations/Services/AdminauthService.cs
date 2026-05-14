using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AMJN_Mosque_Locator.AppDbContext;
using AMJN_Mosque_Locator.DTOs.LoginDtos;
using AMJN_Mosque_Locator.Interfaces.Repositories;
using AMJN_Mosque_Locator.Interfaces.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

namespace AMJN_Mosque_Locator.Implementations.Services
{
    public class AdminauthService : IAdminauthService
    {
        private readonly IAdminauthRepository _repository;
        private readonly IConfiguration _configuration;
        public AdminauthService(IAdminauthRepository repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //public async Task<LoginResponseDto> LoginAsyn(LoginRequestModel dto)
        //{
          
        //}

        public async Task<LoginResponseDto> LoginAsync(LoginRequestModel dto)
        {
            var admin = await _repository.GetByUsernameAsync(dto.UserName);

            if (admin == null)
                return null;

            // ── Step 2: Verify password ───────────────────
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash);

            if (!isPasswordValid)
                return null;

            // ── Step 3: Update last login time ────────────
            admin.LastLoginAt = DateTime.UtcNow;
            await _repository.UpdateLastLoginAsync(admin);

            // ── Step 4: Generate JWT token ─────────────────
            var token = GenerateJwtToken(admin.Username, admin.Email);

            return new LoginResponseDto
            {
                Token = token.Token,
                Expiration = token.Expiration
            };
        }

        // ── JWT Token Generator ───────────────────────────
        private (string Token, DateTime Expiration) GenerateJwtToken(
      string username, string email)
        {
            // FIX: Use JwtTokenSettings (not Jwt) to match your appsettings.json
            var tokenKey = _configuration["JwtTokenSettings:TokenKey"];

            // Handle null email
            email = email ?? username;

            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new InvalidOperationException("JwtTokenSettings:TokenKey is not configured!");
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenKey));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(
                int.Parse(_configuration["JwtTokenSettings:TokenExpiryPeriod"] ?? "1"));

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Email, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtTokenSettings:TokenIssuer"],
                audience: _configuration["JwtTokenSettings:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiration);
        }

    }
}
 