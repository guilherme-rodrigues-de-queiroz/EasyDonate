using EasyDonate.Application.DTOs.Requests.Donors;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Donors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyDonate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonorResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonorResponseDTO>>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonorResponseDTO>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonorResponseDTO>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonorResponseDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _donorService.GetAllDonorsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _donorService.GetDonorByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateDonorDTO dto)
        {
            var response = await _donorService.CreateDonorAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Admin")]
        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<DonorResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDonorDTO dto)
        {
            var response = await _donorService.UpdateDonorAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
