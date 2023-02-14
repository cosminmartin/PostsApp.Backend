using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PostsApp.Domain.Entities;
using PostsApp.Domain.Services;
using PostsApp.Services.Dtos;
using PostsApp.Services;


namespace PostsApp.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var user = await _userService.GetUser(userId);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _userService.CreateUser(user);

            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _userService.UpdateUser(user);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            _userService.DeleteUser(userId);

            return Ok();
        }




        //private readonly IConfiguration _config;

        //private readonly IMapper _mapper;
        //public UserController(IConfiguration config)
        //{
        //    _config = config;   
        //}

        //public UserController(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<UserDto>>>GetAllUsers()
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        //    //var users = await connection.QueryAsync<User>("SELECT * FROM Users");
        //    IEnumerable<UserDto> users = await GetAllUsers(connection);

        //    return Ok(users);
        //}

        //private static async Task<IEnumerable<UserDto>> GetAllUsers(SqlConnection connection)
        //{
        //    return await connection.QueryAsync<UserDto>("SELECT * FROM Users");
        //}


        //[HttpGet("{userId}")]
        //public async Task<ActionResult<List<UserDto>>> GetUser(Guid userId)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    var user = await connection.QueryFirstAsync<UserDto>("SELECT * FROM Users WHERE Id = @UserId",
        //        new {UserId = userId}               
        //        );

        //    return Ok(user);
        //}


        //[HttpPost]
        //public async Task<ActionResult<List<UserDto>>> CreateUser(UserDto user)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    await connection.ExecuteAsync("INSERT INTO Users (FirstName, LastName, Phonenumber, Email) values(@FirstName, @LastName, @Phonenumber, @Email)", user);
        //    return Ok(await GetAllUsers(connection));
        //}


        //[HttpPut]
        //public async Task<ActionResult<List<UserDto>>> UpdateUser(UserDto user)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    await connection.ExecuteAsync("UPDATE Users SET firstName = @FirstName, lastName = @LastName, phonenumber = @Phonenumber, email = @Email WHERE id = @Id ", user);
        //    return Ok(await GetAllUsers(connection));
        //}


        //[HttpDelete("{userId}")]
        //public async Task<ActionResult<List<UserDto>>> DeleteUser(Guid userId)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @UserId ", new {UserId = userId});
        //    return Ok(await GetAllUsers(connection));
        //}

    }
}
