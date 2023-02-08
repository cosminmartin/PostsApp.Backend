namespace PostsApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public List<Post> Posts { get; set; }

        public List<Message> Messages { get; set; }
        public User()
        {
            Posts = new List<Post>();
            Messages = new List<Message>();
        }
    }
}
