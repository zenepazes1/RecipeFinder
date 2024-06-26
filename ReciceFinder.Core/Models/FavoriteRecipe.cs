﻿namespace RecipeFinder.Core.Models
{
    public class FavoriteRecipe
    {
        public string UserId { get; set; } = default!;

        public int RecipeId { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual Recipe? Recipe { get; set; }
    }
}
