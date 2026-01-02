using EasyDonate.Application.DTOs.Requests.Users;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyDonate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpPatch("Inactivate/{email}")]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Inativar(string email, [FromBody] UpdateUserDTO dto)
        {
            var response = await _userService.InativeUserAsync(email, dto);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Donor,Ong,Admin")]
        [HttpPatch("Activate/{email}")]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ResponseModel<UserResponseDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Activate(string email, [FromBody] UpdateUserDTO dto)
        {
            var response = await _userService.ActivateUserAsync(email, dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
