using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;

namespace SalernoServer.Controllers
{
    [Route("api/item")]
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
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(string guid)
        {
            var item = await _context.Items.FindAsync(guid);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(string guid, Item item)
        {
            if (guid != item.GUID)
            {
                return BadRequest();
            }
            var newItem = await _context.Items.FindAsync(guid);
            if (newItem == null)
            {
                return NotFound();
            }
            newItem.GUID = item.GUID;
            //newItem.ItemUUID = item.ItemUUID;
            newItem.Name = item.Name;
            newItem.Department = item.Department;
            newItem.Category = item.Category;
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

            // _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ItemExists(guid))
            {
                return NotFound();
            }

            return NoContent();
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
                new { id = item.GUID }, 
                item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string guid)
        {
            var item = await _context.Items.FindAsync(guid);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(string guid)
        {
            return _context.Items.Any(e => e.GUID == guid);
        }

    }
}
