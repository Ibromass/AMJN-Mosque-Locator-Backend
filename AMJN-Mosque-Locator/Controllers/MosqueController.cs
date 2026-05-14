using AMJN_Mosque_Locator.DTOs;
using AMJN_Mosque_Locator.DTOs.MosqueDtos;
using AMJN_Mosque_Locator.Entities;
using AMJN_Mosque_Locator.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace AMJN_Mosque_Locator.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/")]
    public class MosqueController : ControllerBase
    {
        private readonly IMosqueService _service;

        public MosqueController(IMosqueService service)
        {
            _service = service;
        }
        //[Authorize]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MosqueDto>>> GetAll()
        {
            var mosques = await _service.GetAllAsync();
            return Ok(mosques);
        }

        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<MosqueDto>> GetById(Guid id)
        {
            var mosque = await _service.GetByIdAsync(id);

            if (mosque == null)
                return NotFound(new { message = $"Mosque with ID {id} not found." });

            return Ok(mosque);
        }

        [AllowAnonymous]
        [HttpGet("nearby")]
        public async Task<ActionResult<IEnumerable<MosqueDto>>> GetNearby([FromQuery] NearbySearchDto query)
        {
            query.Latitude = 6.5244;
            query.Longitude = 3.3792;
            query.RadiusKm = 50;

            Console.WriteLine("[Controller] Using TEST Lagos location");
            Console.WriteLine($"[Controller] User Lat: {query.Latitude}, Lng: {query.Longitude}, Radius: {query.RadiusKm}");
            var mosques = await _service.GetNearbySearchAsync(query);

            if (!mosques.Any())
            {
                // No mosques found - return 200 with message
                return Ok(new
                {
                    message = "No Ahmadiyya mosques found near your location.",
                    data = new List<MosqueDto>()
                });
            }

            // Mosques found - return 200 with data (NOT NoContent!)
            return Ok(new
            {
                message = $"{mosques.Count()} mosque(s) found",
                data = mosques
            });
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
 
        [HttpPost]
        public async Task<ActionResult<MosqueDto>> Create([FromBody] CreateMosqueRequestModel dto)
        {

            var mosque = await _service.CreateAsync(dto);
            return Ok(new
            {
                message = "Mosque added successfully",
                data = mosque
            });
        }
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
       public async Task<ActionResult<MosqueDto>> Update(Guid id, [FromBody] UpdateMosqueRequestModel dto)
       {
        
         var mosque = await _service.UpdateAsync(id, dto);

        if (mosque == null)
        return NotFound(new { message = $"Mosque with ID {id} not found." });

        return Ok(mosque);
       }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
       {
        var result = await _service.DeleteAsync(id);

         if (!result)
           return NotFound(new { message = $"Mosque with ID {id} not found." });

          return Ok(new { message = "Mosque has been deactivated successfully." });
        }

    }
}