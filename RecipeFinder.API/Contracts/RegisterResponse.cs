namespace RecipeFinder.API.Contracts
{
    public record RegisterResponse(
        string UserId,
        string Email,
        string FirstName,
        string LastName,
        bool Success,
        string Message);
}
