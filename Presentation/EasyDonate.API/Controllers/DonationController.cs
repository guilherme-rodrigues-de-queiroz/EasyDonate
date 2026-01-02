using EasyDonate.Application.DTOs.Requests.Donation;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Donations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyDonate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _donationService.GetAllDonationsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Ong,Admin")]
        [HttpGet("Ong/{ongId:int}")]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOngDonation(int ongId)
        {
            var response = await _donationService.GetDonationByOngIdAsync(ongId);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Admin")]
        [HttpGet("Donor/{donorId:int}")]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<IReadOnlyList<DonationResponseDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDonorDonation(int donorId)
        {
            var response = await _donationService.GetDonationByDonorIdAsync(donorId);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<DonationResponseDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseModel<DonationResponseDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<DonationResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<DonationResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<DonationResponseDTO>), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ResponseModel<DonationResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateDonationDTO dto)
        {
            var response = await _donationService.CreateDonationAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
