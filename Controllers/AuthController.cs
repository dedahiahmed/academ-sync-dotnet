using academ_sync_back.Models;
using academ_sync_back.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace academ_sync_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                await _userService.CreateUserAsync(user);
                return Ok(new { message = "User registered successfully" });
            }
            catch (ArgumentException ex)
            {
                if (ex.Message == "Email already exists")
                {
                    return BadRequest(new { message = ex.Message });
                }
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.AuthenticateAsync(request.Username, request.Password);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetMe()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return Unauthorized();
            }

            var claims = claimsIdentity.Claims.ToList();
            var userIdClaim = claims.Count >= 3 ? claims[2].Value : null;
            var emailClaim = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var roleClaim = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;

            if (userIdClaim == null || emailClaim == null || roleClaim == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                id = userIdClaim,
                email = emailClaim,
                role = roleClaim
            });
        }
    }
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }

