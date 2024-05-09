namespace RecipeFinder.API.Contracts
{
    public record UserResponse(
        int UserId, 
        string Username, 
        string Email);

}
