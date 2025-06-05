using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
/*using Microsoft.IdentityModel.JsonWebTokens;*/
using TradeMarket.Models;
using TradeMarket.Models.Dto;
using TradeMarket.IRepository;
using System.Net;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TradeMarket.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ApiResponse _response;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthController(ILogger<AuthController> logger, IConfiguration config, IUserRepository userRepo)
        {
            _logger = logger;
            _config = config;
            _response = new ApiResponse();
            _userRepo = userRepo;
        }


        [HttpPost("login", Name = "UserLogin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> UserLogin([FromBody] UserLoginDto loginDto, CancellationToken cancellationToken)
        {

            var user = await _userRepo.FindAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());

            if (user == null)
            {
                ModelState.AddModelError(nameof(loginDto.Email),
                    "Incorrect password please try again.");

                {
                    _logger.LogError(ModelState.ToString());
                    _response.Result = false;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { ModelState.ToString() };
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
            }
            var pwAttempt = Utilities.Utils.VerifyUserPassword(loginDto.Password, user.Password);

            {
                var loginResult = pwAttempt ? GenerateJwtToken(user.Email, user.UserId.ToString()) : "Unauthorized";
                _response.Result = loginResult;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
        }

        private string GenerateJwtToken(string email, string userId)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("role","Trader"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
          };


            var identity = new ClaimsIdentity(claims);

            var secretKey = _config["JwtSettings:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
