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

            await _context.Categories.AddAsync(categoryEntity);
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
            return await _context.Categories
                .AsNoTracking()
                .Select(c => new Category
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                }).ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var categoryEntity = await _context.Categories.FindAsync(category.CategoryId);

            if (categoryEntity != null)
            {
                categoryEntity.Name = category.Name;
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
    }
}
