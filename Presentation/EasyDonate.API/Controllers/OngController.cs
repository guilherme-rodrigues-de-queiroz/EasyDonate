using EasyDonate.Application.DTOs.Requests.Ongs;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Ongs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyDonate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OngController : ControllerBase
    {
        private readonly IOngService _ongService;

        public OngController(IOngService ongService)
        {
            _ongService = ongService;
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<OngResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<OngResponseDTO>>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<OngResponseDTO>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<OngResponseDTO>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<OngResponseDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ongService.GetAllOngsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _ongService.GetOngByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateOngDTO dto)
        {
            var response = await _ongService.CreateOngAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Ong,Admin")]
        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<OngResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOngDTO dto)
        {
            var response = await _ongService.UpdateOngAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
