﻿namespace RecipeFinder.API.Contracts
{
    public record UserRequest(
        string Username, 
        string Email, 
        string Password);

}
