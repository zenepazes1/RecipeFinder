namespace RecipeFinder.API.Contracts
{
    public record UserResponse(
        string UserId,
        string Email,
        string FirstName,
        string LastName);

}
