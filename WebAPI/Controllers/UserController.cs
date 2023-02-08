using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PostsApp.Domain.Entities;
using PostsApp.Services.Dtos;

namespace PostsApp.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;   
        }


        //GET USERS
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>>GetAllUsers()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            //var users = await connection.QueryAsync<User>("SELECT * FROM Users");
            IEnumerable<UserDto> users = await GetAllUsers(connection);

            return Ok(users);
        }

        private static async Task<IEnumerable<UserDto>> GetAllUsers(SqlConnection connection)
        {
            return await connection.QueryAsync<UserDto>("SELECT * FROM Users");
        }

        //GET USER
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<UserDto>>> GetUser(Guid userId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var user = await connection.QueryFirstAsync<UserDto>("SELECT * FROM Users WHERE Id = @UserId",
                new {UserId = userId}               
                );

            return Ok(user);
        }

        //CREATE
        [HttpPost]
        public async Task<ActionResult<List<UserDto>>> CreateUser(UserDto user)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("INSERT INTO Users (FirstName, LastName, Phonenumber, Email) values(@FirstName, @LastName, @Phonenumber, @Email)", user);
            return Ok(await GetAllUsers(connection));
        }

        //UPDATE
        [HttpPut]
        public async Task<ActionResult<List<UserDto>>> UpdateUser(UserDto user)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("UPDATE Users SET firstName = @FirstName, lastName = @LastName, phonenumber = @Phonenumber, email = @Email WHERE id = @Id ", user);
            return Ok(await GetAllUsers(connection));
        }

        //DELETE
        [HttpDelete("{userId}")]
        public async Task<ActionResult<List<UserDto>>> DeleteUser(Guid userId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("DELETE FROM Users WHERE Id = @UserId ", new {UserId = userId});
            return Ok(await GetAllUsers(connection));
        }

    }
}
