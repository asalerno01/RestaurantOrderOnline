using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;

namespace SalernoServer.Controllers
{
    [Route("api/modifier")]
    [ApiController]
    public class ModiferController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModiferController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/modifiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modifier>>> GetModififers()
        {
            // might not work
            return await _context.Modifiers
                .Include(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(m => m.Addons)
                .Include(m => m.NoOptions)
                .ToListAsync();
        }

        // GET: api/modifier/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modifier>> GetModifier(long id)
        {
            var modifier = await _context.Modifiers
                .Include(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(m => m.Addons)
                .Include(m => m.NoOptions)
                .Where(m => m.ModifierId == id)
                .FirstOrDefaultAsync();

            if (modifier == null)
            {
                return NotFound();
            }

            return modifier;
        }
        // GET: api/modifier/5
        [HttpGet("item/{guid}")]
        public async Task<ActionResult<Modifier>> GetItemModifier(string guid)
        {
            var item = await _context.Items.FindAsync(guid);
            if (item == null)
            {
                return NotFound();
            }
            var modifier = await _context.Modifiers
                .Include(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(m => m.Addons)
                .Include(m => m.NoOptions)
                .Where(m => m.Item == item)
                .FirstOrDefaultAsync();

            if (modifier == null)
            {
                return NotFound();
            }

            return modifier;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModifier(long id, [FromBody] Modifier modifierNew)
        {

            if (id != modifierNew.ModifierId)
            {
                return BadRequest();
            }
            var modifier = await _context.Modifiers.FindAsync(id);
            if (modifier == null)
            {
                return NotFound();
            }
            Console.WriteLine("Removing modifier ID:{0}", id);
            _context.Modifiers.Remove(modifier);
            Console.WriteLine("Success!");
            Console.WriteLine("Adding new modifier");
            _context.Modifiers.Add(modifierNew);
            Console.WriteLine("Success!");
            /*modifier.Name = modifierNew.Name;
            modifier.Description = modifierNew.Description;
            modifier.Addons = modifierNew.Addons;
            modifier.NoOptions = modifierNew.NoOptions;
            modifier.Groups = modifierNew.Groups;*/

            Console.WriteLine("Done");

            /*            _context.Entry(modifier).State = EntityState.Modified;*/

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ModifierExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/modifiers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostModifier([FromBody] ModifierHelper modifier)
        {
            if (modifier == null)
            {
                return BadRequest();
            }
            var foundItem = await _context.Items.FindAsync(modifier.ItemGUID);
            if (foundItem == null) return BadRequest($"Modifier ItemID {modifier.ItemGUID} not found");
            var newModifier = new Modifier
            {
                Name = modifier.Name,
                Description = modifier.Description,
                Item = foundItem
            };
            await _context.Modifiers.AddAsync(newModifier);
            if (modifier.Addons.Any())
            {
                foreach (var addon in modifier.Addons)
                {
                    if (addon is not null)
                    {
                        // TODO: Could check if addon exists already...
                        var newAddon = new Addon
                        {
                            Name = addon.Name,
                            Price = addon.Price,
                            Modifier = newModifier
                        };
                        await _context.Addons.AddAsync(newAddon);
                    }
                }
            }
            if (modifier.NoOptions.Any())
            {
                foreach (var noOption in modifier.NoOptions)
                {
                    if (noOption is not null)
                    {
                        // TODO: Could check if noOption exists already...
                        var newNoOption = new NoOption
                        {
                            Name = noOption.Name,
                            DiscountPrice = noOption.DiscountPrice,
                            Modifier = newModifier
                        };
                        await _context.NoOptions.AddAsync(newNoOption);
                    }
                }
            }
            if (modifier.Groups.Any())
            {
                foreach (var group in modifier.Groups)
                {
                    if (group is not null)
                    {
                        // TODO: Could check if group exists already...
                        var newGroup = new Group
                        {
                            Name = group.Name,
                            Modifier = newModifier
                        };
                        await _context.Groups.AddAsync(newGroup);
                        if (group.GroupOptions.Any())
                        {
                            foreach (var groupOption in group.GroupOptions)
                            {
                                var newGroupOption = new GroupOption
                                {
                                    Name = groupOption.Name,
                                    Price = groupOption.Price,
                                    Group = newGroup
                                };
                                await _context.GroupOptions.AddAsync(newGroupOption);
                            }
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetModifier),
                new { id = newModifier.ModifierId },
                newModifier);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var modifier = await _context.Modifiers.FindAsync(id);
            if (modifier == null)
            {
                return NotFound();
            }

            _context.Modifiers.Remove(modifier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModifierExists(long id)
        {
            return _context.Modifiers.Any(e => e.ModifierId == id);
        }
    }
}
