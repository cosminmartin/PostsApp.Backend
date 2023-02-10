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

        [HttpGet("{messageId}")]
        public async Task<ActionResult<List<MessageDto>>> GetMessage(Guid messageId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var message = await connection.QueryFirstAsync<MessageDto>("SELECT * FROM Messages WHERE Id = @MessageId",
                new { MessageId = messageId }
                );

            return Ok(message);
        }

     
        [HttpPut]
        public async Task<ActionResult<List<MessageDto>>> UpdateMessage(MessageDto message)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("UPDATE Messages SET content = @Content WHERE id = @Id ", message);
            return Ok(await GetAllMessages(connection));
        }

 
        [HttpDelete("{messageId}")]
        public async Task<ActionResult<List<MessageDto>>> DeleteMessage(Guid messageId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("DELETE FROM Messages WHERE Id = @MessageId ", new { MessageId = messageId });
            return Ok(await GetAllMessages(connection));
        }
    }
}
