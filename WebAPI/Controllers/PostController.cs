using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PostsApp.Services.Dtos;

namespace PostsApp.WebAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _config;
        public PostController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetAllPosts()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            IEnumerable<PostDto> posts = await GetAllPosts(connection);

            return Ok(posts);
        }

        private static async Task<IEnumerable<PostDto>> GetAllPosts(SqlConnection connection)
        {
            return await connection.QueryAsync<PostDto>("SELECT * FROM Posts");
        }


        [HttpGet("{postId}")]
        public async Task<ActionResult<List<PostDto>>> GetPost(Guid postId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            var post = await connection.QueryFirstAsync<PostDto>("SELECT * FROM Posts WHERE Id = @PostId",
                new { PostId = postId }
                );

            return Ok(post);
        }


        //[HttpPost]
        //public async Task<ActionResult<List<PostDto>>> CreatePost(PostDto post)
        //{
        //    using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        //    await connection.ExecuteAsync("INSERT INTO Posts (Content, UserId) values(@Content, @UserId)", post);
        //    return Ok(await GetAllPosts(connection));
        //}


        [HttpPut]
        public async Task<ActionResult<List<PostDto>>> UpdatePost(PostDto post)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("UPDATE Posts SET content = @Content WHERE id = @Id ", post);
            return Ok(await GetAllPosts(connection));
        }


        [HttpDelete("{postId}")]
        public async Task<ActionResult<List<PostDto>>> DeletePost(Guid postId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync("DELETE FROM Posts WHERE Id = @PostId ", new { PostId = postId });
            return Ok(await GetAllPosts(connection));
        }
    }
}
