using TradeMarket.IRepository;
using TradeMarket.Models.Dto;
using TradeMarket.Data;
using TradeMarket.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TradeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepo;
        private readonly ILogger _logger;
        private readonly ApiResponse _response;

        public UserController(ILogger<UserController> logger, IUserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _response = new ApiResponse();
        }

        [HttpPost]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] UserCreateDto createDto)
        {
            if (await _userRepo.FindAsync(u => u.Email.ToLower() == createDto.Email.ToLower()) != null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("User already exists");
                _response.StatusCode = HttpStatusCode.BadRequest;

            }

            if (createDto == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Problem with input, please try again.");
                _response.StatusCode = HttpStatusCode.BadRequest;

            }
            var user = UserMappers.ToUserFromUserCreateDto(createDto);
            await _userRepo.CreateAsync(user);

            _response.Result = user;
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            return _response;

        }
    }
}
