﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;
using Server.Models;
using Server.Models.Authentication;

namespace SalernoServer.Controllers
{
    [Route("api/orders")]
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
            var orders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Addons)
                    .ThenInclude(oia => oia.Addon)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.NoOptions)
                    .ThenInclude(oino => oino.NoOption)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Groups)
                    .ThenInclude(oig => oig.Group)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Groups)
                    .ThenInclude(oig => oig.GroupOption)
                    .Include(o => o.Account)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Item)
                    .ToListAsync();

            List<OrderDTO> ordersDTO = new();
            foreach (var order in orders)
            {
                ordersDTO.Add(OrderToOrderDTO(order));
            }
            return Ok(ordersDTO);
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            Console.WriteLine("=========" + id);
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Addons)
                .ThenInclude(a => a.Addon)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.NoOptions)
                .ThenInclude(no => no.NoOption)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Groups)
                .ThenInclude(g => g.GroupOption)
                .Include (o => o.Account)
                .Where(o => o.OrderId == id)
                .FirstOrDefaultAsync();

            if (order == null) return NotFound();

            return Ok(OrderToOrderDTO(order));
        }
        [HttpGet]
        [Route("savedorders/{id}")]
        public async Task<ActionResult<List<SavedOrderDTO>>> GetSavedOrders(long id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account is null) return NoContent();
            var savedOrders = await _context.SavedOrders
                    .Include(savedOrder => savedOrder.OrderItems)
                    .ThenInclude(orderItem => orderItem.OrderItem)
                    .ThenInclude(orderItem => orderItem.Addons)
                    .ThenInclude(addons => addons.Addon)
                    .Include(savedOrder => savedOrder.OrderItems)
                    .ThenInclude(orderItem => orderItem.OrderItem)
                    .ThenInclude(orderItem => orderItem.NoOptions)
                    .ThenInclude(noOption => noOption.NoOption)
                    .Include(savedOrder => savedOrder.OrderItems)
                    .ThenInclude(orderItem => orderItem.OrderItem)
                    .ThenInclude(orderItem => orderItem.Groups)
                    .ThenInclude(group => group.Group)
                    .Include(savedOrder => savedOrder.OrderItems)
                    .ThenInclude(orderItem => orderItem.OrderItem)
                    .ThenInclude(orderItem => orderItem.Groups)
                    .ThenInclude(group => group.GroupOption)
                    .Include(savedOrder => savedOrder.OrderItems)
                    .ThenInclude(orderItem => orderItem.OrderItem)
                    .Include(savedOrder => savedOrder.OrderItems)
                    .ThenInclude(savedOrderItem => savedOrderItem.OrderItem)
                    .ThenInclude(orderItem => orderItem.Item)
                    .Where(so => so.Account == account)
                    .ToListAsync();

            //Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(savedOrders));
            List<SavedOrderDTO> savedOrderDTOList = new();
            foreach(SavedOrder savedOrder in savedOrders)
            {
                List<SavedOrderOrderItemDTO> orderItems = new();
                foreach(SavedOrderOrderItem savedOrderItem in savedOrder.OrderItems)
                {
                    orderItems.Add(new()
                    {
                        ItemId = savedOrderItem.OrderItem.ItemId,
                        ItemName = savedOrderItem.OrderItem.Item.Name,
                        Price = savedOrderItem.OrderItem.Item.Price,
                        Count = savedOrderItem.OrderItem.Count,
                        Addons = savedOrderItem.OrderItem.Addons,
                        NoOptions = savedOrderItem.OrderItem.NoOptions,
                        Groups = savedOrderItem.OrderItem.Groups
                    });
                }
                savedOrderDTOList.Add(new()
                {
                    SavedOrderName = savedOrder.Name,
                    OrderItems = orderItems
                });

            }
            return Ok(savedOrderDTOList);

        }
        private static List<SavedOrderOrderItemDTO> OrderItemsToSavedOrderOrderItemsDTO(List<SavedOrderOrderItem> savedOrderOrderItems)
        {
            List<SavedOrderOrderItemDTO> savedOrderItemsDTO = new();
            foreach (var savedOrderOrderItem in savedOrderOrderItems)
            {
                foreach (var orderItem in savedOrderOrderItems)
                {
                    savedOrderItemsDTO.Add(new()
                    {
                        ItemId = orderItem.OrderItem.ItemId,
                        ItemName = orderItem.OrderItem.Item.Name,
                        Count = orderItem.OrderItem.Count,
                        Addons = orderItem.OrderItem.Addons,
                        NoOptions = orderItem.OrderItem.NoOptions,
                        Groups = orderItem.OrderItem.Groups
                    });
                };
            }
            return savedOrderItemsDTO;
        }


            // PUT: api/Items/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromBody] OrderHelper order)
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

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderHelper order)
        {
            if (order is null) return BadRequest("Invalid order");
            var newOrder = new Order
            {
                Subtotal = order.Subtotal,
                SubtotalTax = order.SubtotalTax,
                Total = order.Total
            };
            var account = await _context.Accounts.FindAsync(order.AccountId);
            if (account is null) Console.WriteLine("Creating order for null customer account.");
            newOrder.Account = account;
            if (order.OrderItems.IsNullOrEmpty()) return BadRequest("Order must have at least 1 order item.");
            foreach (var orderItem in order.OrderItems)
            {
                var foundItem = await _context.Items.FindAsync(orderItem.ItemId);
                if (foundItem is null) return BadRequest($"Cannot find item with ID => {orderItem.ItemId}");
                var newOrderItem = new OrderItem
                {
                    Order = newOrder,
                    Item = foundItem
                };
                foreach (var groupOption in orderItem.GroupOptions)
                {
                    var foundGroup = await _context.Groups.FindAsync(groupOption.GroupId);
                    if (foundGroup is null) return BadRequest($"GroupID {groupOption.GroupId} is not a group.");
                    var foundGroupOption = await _context.GroupOptions.FindAsync(groupOption.GroupOptionId);
                    if (foundGroupOption is null) return BadRequest($"GroupOptionID {groupOption.GroupOptionId} is not a group option.");
                    if (!foundGroup.GroupOptions.Any(go => go.GroupOptionId == foundGroupOption.GroupOptionId)) return BadRequest($"GroupID {foundGroup.GroupId} does not contain a group option with ID {foundGroupOption.GroupOptionId}");

                    var newOrderItemGroup = new OrderItemGroup
                    {
                        OrderItem = newOrderItem,
                        Group = foundGroup,
                        GroupOption = foundGroupOption
                    };
                    PrintGroup(newOrderItemGroup);
                    newOrderItem.Groups.Add(newOrderItemGroup);
                }
                foreach (var addon in orderItem.Addons)
                {
                    var foundAddon = await _context.Addons.FindAsync(addon.AddonId);
                    if (foundAddon is null) return BadRequest($"Cannot find addon with ID => {addon.AddonId}");
                    var newOrderItemAddon = new OrderItemAddon
                    {
                        OrderItem = newOrderItem,
                        Addon = foundAddon
                    };
                    PrintAddon(newOrderItemAddon);
                    newOrderItem.Addons.Add(newOrderItemAddon);
                }
                foreach (var noOption in orderItem.NoOptions)
                {
                    var foundNoOption = await _context.NoOptions.FindAsync(noOption.NoOptionId);
                    if (foundNoOption is null) return BadRequest($"Cannot find NoOption with ID => {noOption.NoOptionId}");
                    var newOrderItemNoOption = new OrderItemNoOption
                    {
                        OrderItem = newOrderItem,
                        NoOption = foundNoOption
                    };
                    PrintNoOption(newOrderItemNoOption);
                    newOrderItem.NoOptions.Add(newOrderItemNoOption);
                }
                newOrder.OrderItems.Add(newOrderItem);
            }
            if (order.SaveOrder && order.SavedOrderName is not null && account is not null)
            {
                SavedOrder savedOrder = new()
                {
                    Name = order.SavedOrderName,
                    Account = account
                };
                savedOrder.OrderItems = OrderItemsToSavedOrderOrderItems(savedOrder, newOrder);
                await _context.SavedOrders.AddAsync(savedOrder);
            }
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Added Order => {newOrder.OrderId}");

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = newOrder.OrderId },
                newOrder);
        }
        private static List<SavedOrderOrderItem> OrderItemsToSavedOrderOrderItems(SavedOrder savedOrder, Order order)
        {
            List<SavedOrderOrderItem> savedOrderOrderItems = new();
            foreach(OrderItem orderItem in order.OrderItems)
            {
                savedOrderOrderItems.Add(new()
                {
                    SavedOrder = savedOrder,
                    OrderItem = orderItem
                });
            }
            return savedOrderOrderItems;

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

        private static OrderDTO OrderToOrderDTO(Order order)
        {
            OrderDTO orderDTO = new()
            {
                OrderId = order.OrderId,
                Account = AccountToOrderAccountDTO(order.Account),
                Subtotal = order.Subtotal,
                SubtotalTax = order.SubtotalTax,
                Total = order.Total,
                Status = order.Status,
                OrderDate = order.OrderDate,
                PickupDate = order.PickupDate,
                OrderItems = OrderItemsToOrderItemDTOs(order.OrderItems)
            };
            return orderDTO;
        }
        private static List<OrderItemDTO> OrderItemsToOrderItemDTOs(List<OrderItem> orderItems)
        {
            List<OrderItemDTO> newOrderItems = new();
            foreach (var item in orderItems)
            {
                newOrderItems.Add(new()
                {
                    OrderItemId = item.OrderItemId,
                    OrderId = item.Order.OrderId,
                    ItemId = item.Item.ItemId,
                    ItemName = item.Item.Name,
                    Count = item.Count,
                    Groups = item.Groups,
                    Addons = item.Addons,
                    NoOptions = item.NoOptions
                });
            }
            return newOrderItems;
        }
        private static void PrintAddon(OrderItemAddon addon)
        {
            Console.WriteLine($"ID=>{addon.Addon.AddonId}, Name=>{addon.Addon.Name}, Price=>{addon.Addon.Price}");
        }
        private static void PrintNoOption(OrderItemNoOption noOption)
        {
            Console.WriteLine($"ID=>{noOption.NoOption.NoOptionId}, Name=>{noOption.NoOption.Name}, Price=>{noOption.NoOption.Price}");
        }
        private static void PrintGroup(OrderItemGroup group)
        {
            Console.WriteLine($"ID=>{group.Group.GroupId}, Name=>{group.Group.Name}, GroupOptionId=>{group.GroupOption.GroupOptionId}, GroupOptionName=>{group.GroupOption.Name}, Price=>{group.GroupOption.Price}");
        }
        private static OrderAccountDTO AccountToOrderAccountDTO(Account account)
        {
            if (account is null) return null;
            return new OrderAccountDTO
            {
                AccountId = account.AccountId,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                PhoneNumber = account.PhoneNumber
            };
        }
    }
}
