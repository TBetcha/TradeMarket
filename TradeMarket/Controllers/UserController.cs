using TradeMarket.IRepository;
using TradeMarket.Models.Dto;
using TradeMarket.Data;
using TradeMarket.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NodaTime;

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
                ModelState.AddModelError(nameof(createDto.Email),
                    "Email already exists");
                {
                    _logger.LogError(ModelState.ToString());
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { ModelState.ToString() };
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
            }

            if (createDto == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Problem with input, please try again." };
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
                _response.ErrorMessages = new List<string> { "Invalid User Id" };
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

        [HttpPut("{id:guid}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateDto updateDto)
        {
            /*var existingUser = await _userRepo.GetByIdAsync(id);*/
            var existingUser = await _userRepo.FindAsync(x => x.UserId == id, false);
            if (existingUser == null)
            {

                _logger.LogError("User does not exist: {id}", id);
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "Invalid User Id" };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            //HACK: Need to come up with a real way to only patch certain properties 
            var updatedUser = new User
            {
                UserId = id,
                FirstName = updateDto.FirstName != null ? updateDto.FirstName : existingUser.FirstName,
                LastName = updateDto.LastName != null ? updateDto.LastName : existingUser.LastName,
                Email = existingUser.Email,
                DateOfBirth = updateDto.DateOfBirth != null ? updateDto.DateOfBirth : existingUser.DateOfBirth,
                Address = existingUser.Address,
                Password = existingUser.Password,
                CreatedAt = existingUser.CreatedAt,
                LastUpdated = Instant.FromDateTimeUtc(System.DateTime.UtcNow)
            };
            await _userRepo.UpdateUserAsync(updatedUser);

            _response.Result = updatedUser;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return _response;

        }

        [HttpPost("login", Name = "UserLogin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> UserLogin([FromBody] UserLoginDto loginDto)
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

            _response.Result = Utilities.Utils.VerifyUserPassword(loginDto.Password, user.Password);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}
