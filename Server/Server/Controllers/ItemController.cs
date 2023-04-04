using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.Helpers;

namespace SalernoServer.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            var items = await _context.Items
                .Include(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(i => i.Category)
                .OrderBy(i => i.Category)
                .ToListAsync();
            List<ItemDTO> result = new List<ItemDTO>();
            foreach (var item in items)
            {
                result.Add(new ItemDTO(item));
            }

            return Ok(result);
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(string id)
        {
            //var item = await _context.Items.FindAsync(itemId);
            var item = await _context.Items
                .Include(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.ItemId.Equals(id));

            if (item == null)
            {
                return NotFound();
            }
            

            return Ok(new ItemDTO(item));
        }
        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(string id, [FromBody] ItemHelper item)
        {
            if (id != item.ItemId)
            {
                return BadRequest($"item.ItemId");
            }
            var newItem = await _context.Items.FindAsync(id);
            if (newItem == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.Where(c => c.Name.Equals(item.CategoryName)).FirstOrDefaultAsync();
            if (category is null)
            {
                Console.WriteLine($"Creating new category: {item.CategoryName}");
                category = new()
                {
                    Name = item.CategoryName
                };
                category.Items.Add(newItem);
            }

            newItem.ItemId = item.ItemId;
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            newItem.Department = item.Department;
            newItem.Category = category; 
            newItem.SKU = item.SKU;
            newItem.UPC = item.UPC;
            newItem.Price = item.Price;
            newItem.Discountable = item.Discountable;
            newItem.Taxable = item.Taxable;
            newItem.TrackingInventory = item.TrackingInventory;
            newItem.Cost = item.Cost;
            newItem.AssignedCost = item.AssignedCost;
            newItem.Quantity = item.Quantity;
            newItem.ReorderTrigger = item.ReorderTrigger;
            newItem.RecommendedOrder = item.RecommendedOrder;
            newItem.LastSoldDate = item.LastSoldDate;
            newItem.Supplier = item.Supplier;
            newItem.LiabilityItem = item.LiabilityItem;
            newItem.LiabilityRedemptionTender = item.LiabilityRedemptionTender;
            newItem.TaxGroupOrRate = item.TaxGroupOrRate;

            _context.Update(newItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetItem), 
                new { id = item.ItemId }, 
                item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
