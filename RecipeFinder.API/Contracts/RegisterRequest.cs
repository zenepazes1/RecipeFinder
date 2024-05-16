namespace RecipeFinder.API.Contracts
{
    public record RegisterRequest(
            string Email,
            string Password,
            string FirstName,
            string LastName);
}
