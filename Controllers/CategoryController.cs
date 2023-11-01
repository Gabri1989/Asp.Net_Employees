using FullStack.API.Data;
using FullStack.API.Models.Domain;
using FullStack.API.Models.DTO;
using FullStack.API.Ripositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
           this.categoryRepository=categoryRepository;
            
        }

        public ICategoryRepository CategoryRepository { get; }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };
            await categoryRepository.CreateAsync(category);
            var respone = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
  
            return Ok(respone);
        }
    }
}
