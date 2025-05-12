using TradeMarket.IRepository;
using TradeMarket.Models.Dto;
using TradeMarket.Data;
using TradeMarket.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TradeMarket.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepo;
        private readonly ILogger _logger;
        private readonly ApiResponse _response;

        public UserController(ILogger<UserController> logger, IUserRepository userRepo, ApplicationDbContext db)
        {
            _logger = logger;
            _userRepo = userRepo;
            _response = new ApiResponse();
            _db = db;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] UserCreateDto createDto)
        {
            if (await _userRepo.FindAsync(u => u.Email.ToLower() == createDto.Email.ToLower()) != null)
            {
                _logger.LogError("User already exists");
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>{"User already exists"};
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (createDto == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>{"Problem with input, please try again."};
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);

            }
            var user = UserMappers.ToUserFromUserCreateDto(createDto!);
            await _userRepo.CreateAsync(user);

            _response.Result = user;
            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            return CreatedAtRoute("GetUserById", new { id = user.UserId }, _response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetAllUsers()
        {
            var userList = await _userRepo.GetAllAsync();
            var userDtoList = userList.Select(user => UserMappers.ToUserDto(user));

            _response.Result = userDtoList;
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpGet("{id:guid}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetUserById([FromRoute] Guid id)
        {

            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("User not found with id {x}", id);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> {"Invalid User Id"};
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            _response.Result = UserMappers.ToUserDto(user);
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpDelete("{id:guid}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> DeleteUser([FromRoute] Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogError("Couldn't delete. User not found with id {x}", id);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid User Id" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            await _userRepo.DeleteAsync(user);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;
            return _response;
        }

    }
}
