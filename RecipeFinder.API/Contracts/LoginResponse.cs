namespace RecipeFinder.API.Contracts
{
    public record LoginResponse(
        string Token,
        string UserId,
        string Email,
        string FirstName,
        string LastName,
        IEnumerable<string> Roles);
}
