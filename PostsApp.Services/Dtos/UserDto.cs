namespace PostsApp.Services.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        //public List<Post> Posts { get; set; }
        //public List<Message> Messages { get; set; }
        
    }
}
