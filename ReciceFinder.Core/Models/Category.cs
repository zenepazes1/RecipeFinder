﻿namespace RecipeFinder.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }

}
