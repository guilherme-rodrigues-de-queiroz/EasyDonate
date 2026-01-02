using EasyDonate.Application.DTOs.Requests.Auth;
using EasyDonate.Application.DTOs.Responses;
using EasyDonate.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyDonate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseModel<TokenResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<TokenResponseDTO>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseModel<TokenResponseDTO>), StatusCodes.Status500InternalServerError)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
