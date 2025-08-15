using JWTTraining.Dtos.Auth;
using JWTTraining.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTTraining.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticationRequestDto authenticationRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            var user = await _userRepository.GetUserAsync(authenticationRequestDto.Username, authenticationRequestDto.Password);
            if (user == null)
            {
                _logger.LogCritical("An Unauthorized User Request For Login With Username : {Username} !", authenticationRequestDto.Username);

                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretForKey"]!));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var jtwSecurityToken = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jtwSecurityToken);
            return Ok(new JsonResult(new { token = tokenToReturn }));
        }
    }
}
