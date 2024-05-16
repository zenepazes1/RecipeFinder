namespace RecipeFinder.API.Contracts
{
    public record LoginResponse(
        string Token,
        string UserId,
        string Email,
        IEnumerable<string> Roles);
}
