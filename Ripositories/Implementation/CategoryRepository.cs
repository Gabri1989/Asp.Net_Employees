using FullStack.API.Data;
using FullStack.API.Models.Domain;
using FullStack.API.Ripositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Ripositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FullStackDBContext dbContext;

        public CategoryRepository(FullStackDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}
