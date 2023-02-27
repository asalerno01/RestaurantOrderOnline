using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SalernoServer.Models;

namespace SalernoServer.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, [FromBody] Order Order)
        {
            //if (id != Order.OrderId)
            //{
            //    return BadRequest();
            //}

            //var order = await _context.Orders.FindAsync(id);
            //if (order == null)
            //{
            //    return NotFound();
            //}

            //// _context.Entry(Order).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (Exception)
            //{
            //    return NotFound();
            //}

            return NoContent();
        }

        // POST: api/orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderHelper order)
        {
            Order newOrder = new();
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            Console.WriteLine("OrderID: {0}", newOrder.OrderId);
            if (order is not null)
            {
                if (!order.OrderItems.IsNullOrEmpty())
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        var foundItem = await _context.Items.FindAsync(orderItem.ItemGUID);
                        if (foundItem is not null)
                        {
                            var newOrderItem = new OrderItem
                            {
                                Order = newOrder,
                                Item = foundItem
                            };
                            decimal subtotal = 0;
                            await _context.OrderItems.AddAsync(newOrderItem);
                            if (!orderItem.GroupOptions.IsNullOrEmpty())
                            {
                                foreach (var groupOption in orderItem.GroupOptions)
                                {
                                    if (groupOption is not null)
                                    {
                                        var foundGroupOption = await _context.GroupOptions.FindAsync(groupOption.GroupOptionId);
                                        if (foundGroupOption is not null)
                                        {
                                            var newOrderItemGroupOption = new OrderItemGroupOption
                                            {
                                                OrderItem = newOrderItem,
                                                GroupOption = foundGroupOption
                                            };
                                            subtotal += foundGroupOption.Price;
                                            await _context.OrderItemGroupOptions.AddAsync(newOrderItemGroupOption);
                                        }
                                        else
                                        {
                                            return BadRequest($"Cannot find group option with id: {groupOption.GroupOptionId}");
                                        }
                                    }
                                }
                            }
                            if (!orderItem.Addons.IsNullOrEmpty())
                            {
                                foreach (var addon in orderItem.Addons)
                                {
                                    if (addon is not null)
                                    {
                                        var foundAddon = await _context.Addons.FindAsync(addon.AddonId);
                                        if (foundAddon is not null)
                                        {
                                            var newOrderItemAddon = new OrderItemAddon
                                            {
                                                OrderItem = newOrderItem,
                                                Addon = foundAddon
                                            };
                                            await _context.OrderItemAddons.AddAsync(newOrderItemAddon);
                                        }
                                        else
                                        {
                                            return BadRequest($"Cannot find addon with id: {addon.AddonId}");
                                        }
                                    }
                                }
                                if (!orderItem.NoOptions.IsNullOrEmpty())
                                {
                                    foreach (var noOption in orderItem.NoOptions)
                                    {
                                        if (noOption is not null)
                                        {
                                            var foundNoOption = await _context.NoOptions.FindAsync(noOption.NoOptionId);
                                            if (foundNoOption is not null)
                                            {
                                                var newOrderItemNoOption = new OrderItemNoOption
                                                {
                                                    OrderItem = newOrderItem,
                                                    NoOption = foundNoOption
                                                };
                                                await _context.OrderItemNoOptions.AddAsync(newOrderItemNoOption);
                                            }
                                            else
                                            {
                                                return BadRequest($"Cannot find addon with id: {noOption.NoOptionId}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return BadRequest($"Cannot find item with id: {orderItem.ItemGUID}");
                        }
                    }
                }
                else
                {
                    return BadRequest("Order must have at least 1 order item.");
                }
            }
            else
            {
                return BadRequest("Invalid order");
            }

            await _context.SaveChangesAsync();
            Console.WriteLine($"Added Order => {newOrder.OrderId}");

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = newOrder.OrderId },
                newOrder);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
