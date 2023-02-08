namespace PostsApp.Services.Dtos
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
    }
}
