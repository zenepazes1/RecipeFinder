using RecipeFinder.Core.Models;

namespace RecipeFinder.Core.Abstractions
{
    public interface IRecipeRepository
    {
        Task<int> Create(Recipe recipe);
        Task<int> Delete(int id);
        Task<List<Recipe>> Get();
        Task<int> Update(Recipe recipe);

    }
}
