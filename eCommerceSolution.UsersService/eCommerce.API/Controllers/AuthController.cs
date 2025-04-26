using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")] //api/auth
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpPost("Register")] //api/auth/Register
        public async Task<IActionResult?> Register(RegisterRequest registerRequest)
        {
            if(registerRequest == null)
            {
                return BadRequest("Invalid registration data");
            }
            AuthenticationResponse authenticationResponse = await _usersService.Register(registerRequest);
            if(authenticationResponse == null || !authenticationResponse.Success)
            {
                return BadRequest(authenticationResponse);
            }
            return Ok(authenticationResponse);
        }
        [HttpPost("Login")] //api/auth/login
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if(loginRequest == null)
            {
                return BadRequest("Invalid login data");
            }
            AuthenticationResponse authenticationResponse = await _usersService.Login(loginRequest);
            if(authenticationResponse == null || !authenticationResponse.Success)
            {
                return Unauthorized(authenticationResponse);
            }
            return Ok(authenticationResponse);
        }
    }
}
