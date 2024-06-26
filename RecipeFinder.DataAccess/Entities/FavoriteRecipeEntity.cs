﻿namespace RecipeFinder.DataAccess.Entities
{
    public class FavoriteRecipeEntity
    {
        public string UserId { get; set; } = default!;
        public int RecipeId { get; set; }
        public virtual ApplicationUserEntity? User { get; set; }
        public virtual RecipeEntity? Recipe { get; set; }
    }
}
