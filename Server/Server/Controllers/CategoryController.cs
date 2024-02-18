using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalernoServer.Models;
using Server.Models.ItemModels.Helpers;
using Server.Models.ItemModels;

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

            return Ok(categories.Select(category => new CategoryDTO(category)).ToList());
        }
        [HttpGet]
        [Route("simple")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesSimple()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories.Select(category => new CategoryDTO(category)).ToList());
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

            if (category is null) return NotFound();

            return Ok(new CategoryDTO(category));
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category is null) return BadRequest("Category is null");
            var foundCategory = await _context.Categories.Where(c => c.Name.Equals(category.Name)).FirstOrDefaultAsync();
            if (foundCategory is not null) return BadRequest($"Category with {category.Name} already exists");
            
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditCategory(int? id, Category category)
        {
            if (id is null) return NotFound();

            if (id != category.CategoryId) return BadRequest();

            _context.Categories.Update(category);

            // await _context.SaveChangesAsync();

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                Console.WriteLine("Saving");
                await _context.SaveChangesAsync();
                Console.WriteLine("Saved");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Categories.Any(c => c.CategoryId == id)) return NotFound();
                else throw;
            }

            return Ok();
        }

		// POST: Category/Delete/5
		[HttpPost, ActionName("Delete")]
        [Route("delete/{categoryId}")]
		public async Task<IActionResult> DeleteConfirmed(long categoryId)
		{
			var category = await _context.Categories.FindAsync(categoryId);
			if (category != null)
			{
				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();
			}
            return Ok();
		}
	}
}