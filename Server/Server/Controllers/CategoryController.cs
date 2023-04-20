using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SalernoServer.JwtHelpers;
using SalernoServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Nodes;
using SalernoServer.Models.Authentication;
using Server.Models.ItemModels.Helpers;
using Server.Models.ItemModels;
using Microsoft.AspNetCore.Http.Features;
using SalernoServer.Models.ItemModels;

namespace SalernoServer.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .ToListAsync();
            var categoryDTOList = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoryDTOList.Add(CategoryToCategoryDTO(category));
            }
            return Ok(categoryDTOList);
        }
        [HttpGet]
        [Route("simple")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesSimple()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories.Select(category => new CategoryDTO(category.CategoryId, category.Name, category.Description)).ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(long id)
        {
            //var item = await _context.Items.FindAsync(itemId);
            var category = await _context.Categories
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            var categoryDTO = CategoryToCategoryDTO(category);

            return Ok(categoryDTO);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryHelper category)
        {
            if (category is null) return BadRequest("Category is null");
            var foundCategory = await _context.Categories.Where(c => c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();
            if (foundCategory is not null) return BadRequest($"Category with {category.Name} already exists");
            Category newCategory = new()
            {
                Name = category.Name,
                Description = category.Description,
                Items = new()
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return Ok();
        }
        private static CategoryDTO CategoryToCategoryDTO(Category category)
        {
            var categoryDTO = new CategoryDTO()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
            };
            foreach (var item in category.Items)
            {
                categoryDTO.Items.Add(new CategoryItemDTO(item.ItemId, item.Name, item.Description, item.Price, item.Modifier));
            }
            return categoryDTO;
        }
    }
}