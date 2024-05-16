namespace RecipeFinder.API.Contracts
{
    public record RegisterResponse(
        string UserId,
        string Email,
        bool Success,
        string Message);
}
