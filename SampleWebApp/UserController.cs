using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SampleWebApp
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;

        public UserController(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest loginRequest)
        {
            // Replace this with your actual user authentication logic
            if (IsValidUser(loginRequest))
            {
                var token = _jwtTokenService.GenerateToken(loginRequest.Username);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        // Replace this with your actual user authentication logic
        private bool IsValidUser(UserLoginRequest loginRequest)
        {
            // This is a placeholder for your user authentication logic.
            // Replace this with actual user validation using your data store or identity provider.
            return loginRequest.Username == "user" && loginRequest.Password == "password";
        }

        [Authorize]
        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            var username = User.Identity.Name;
            return Ok(new { username });
        }
    }

}
