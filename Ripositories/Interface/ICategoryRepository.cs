using FullStack.API.Models.Domain;

namespace FullStack.API.Ripositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
    }
}
