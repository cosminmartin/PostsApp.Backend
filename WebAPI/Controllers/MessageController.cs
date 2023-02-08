using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PostsApp.Services.Dtos;

namespace PostsApp.WebAPI.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IConfiguration _config;
        public MessageController(IConfiguration config)
        {
            _config = config;
        }


        //GET Messages
        [HttpGet]
        public async Task<ActionResult<List<MessageDto>>> GetAllMessages()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            IEnumerable<MessageDto> messages = await GetAllMessages(connection);

            return Ok(messages);
        }

        private static async Task<IEnumerable<MessageDto>> GetAllMessages(SqlConnection connection)
        {
            return await connection.QueryAsync<MessageDto>("SELECT * FROM Messages");
        }

        //GET Message
        [HttpGet("{messageId}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessage(Guid messageId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var message = await connection.QueryFirstAsync<MessageDto>("SELECT * FROM Messages WHERE Id = @MessageId",
                new { MessageId = messageId }
                );

            return Ok(message);
        }

        //CREATE
        

        //UPDATE
        [HttpPut]
        public async Task<ActionResult<List<MessageDto>>> UpdateMessage(MessageDto message)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("UPDATE Messages SET content = @Content WHERE id = @Id ", message);
            return Ok(await GetAllMessages(connection));
        }

        //DELETE
        [HttpDelete("{messageId}")]
        public async Task<ActionResult<List<MessageDto>>> DeleteMessage(Guid messageId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("DELETE FROM Messages WHERE Id = @MessageId ", new { MessageId = messageId });
            return Ok(await GetAllMessages(connection));
        }
    }
}
