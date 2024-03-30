namespace RecipeFinder.DataAccess.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }

    }
}
