using EasyDonate.Application.DTOs.Requests.Addresses;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Addresses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyDonate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpGet("Ong/{id:int}")]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOngAddress(int id)
        {
            var response = await _addressService.GetOngAddressByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpGet("Donor/{id:int}")]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDonorAddress(int id)
        {
            var response = await _addressService.GetDonorAddressByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseModel<AddressResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateAddressDTO dto)
        {
            var response = await _addressService.CreateAddressAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
