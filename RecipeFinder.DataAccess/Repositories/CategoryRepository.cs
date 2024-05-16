using Microsoft.EntityFrameworkCore;
using RecipeFinder.Core.Abstractions;
using RecipeFinder.Core.Models;
using RecipeFinder.DataAccess.Entities;

namespace RecipeFinder.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RecipeFinderDbContext _context;

        public CategoryRepository(RecipeFinderDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category category)
        {
            var categoryEntity = new CategoryEntity
            {
                Name = category.Name
            };

            _context.Categories.Add(categoryEntity); // Removed the asynchronous call here for adding to context
            await _context.SaveChangesAsync();

            category.CategoryId = categoryEntity.CategoryId; // Update ID after save
            return category;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var categoryEntity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (categoryEntity == null) return null;

            return new Category
            {
                CategoryId = categoryEntity.CategoryId,
                Name = categoryEntity.Name
            };
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .Select(c => new Category
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                }).ToListAsync();

            return categories;
        }

        public async Task UpdateAsync(Category category)
        {
            var categoryEntity = await _context.Categories.FindAsync(category.CategoryId);

            if (categoryEntity != null)
            {
                categoryEntity.Name = category.Name;
                _context.Categories.Update(categoryEntity); // Explicitly marking the entity as modified
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);

            if (categoryEntity != null)
            {
                _context.Categories.Remove(categoryEntity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Category> GetByNameAsync(string name)
        {
            var lowerName = name.ToLower();
            var categoryEntity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.ToLower() == lowerName);

            if (categoryEntity == null) return null;

            return new Category
            {
                CategoryId = categoryEntity.CategoryId,
                Name = categoryEntity.Name
            };
        }



    }
}
