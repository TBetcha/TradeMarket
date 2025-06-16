using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TradeMarket.Models;
using TradeMarket.Models.Dto;
using TradeMarket.IRepository;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TradeMarket.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ApiResponse _response;
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly IJwtUtils _jwtUtils;

        public AuthController(ILogger<AuthController> logger, IConfiguration config, IUserRepository userRepo, IJwtUtils jwtUtils)
        {
            _logger = logger;
            _config = config;
            _response = new ApiResponse();
            _userRepo = userRepo;
            _jwtUtils = jwtUtils;
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
                var loginResult = pwAttempt ? _jwtUtils.GenerateJwtToken(user.Email, user.UserId.ToString()) : "Unauthorized";
                _response.Result = loginResult;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
        }

    }
}
