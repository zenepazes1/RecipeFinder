namespace RecipeFinder.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

        public User(int userId, string username, string email, string passwordHash, bool isAdmin)
        {
            UserId = userId;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            IsAdmin = isAdmin;
        }
    }

}
