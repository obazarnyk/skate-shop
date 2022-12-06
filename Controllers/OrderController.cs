using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using educational_practice5.Authentication;
using educational_practice5.Database;
using educational_practice5.Models;
using educational_practice5.Repositories.Interfaces;
using Newtonsoft.Json;

namespace educational_practice5.Controllers
{
    
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductRepository _repository;
        private readonly RepositoryContext _context;
        
        public OrderController(UserManager<ApplicationUser> userManager, IProductRepository repository,  IMapper mapper, RepositoryContext context)
        {
            this.userManager = userManager;
            this._context = context;
            this._repository = repository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            IdentityUser user = userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
            return Ok(_context.orders.Where(obj => obj.UserId == user.Id)
                .Select(order => new
                {   
                    order.CreatedAt,
                    Products = order.Products.Select(ob => new
                    {
                        Title = _context.products.FirstOrDefault(prod => prod.ProductId == ob.ProductId).Title,
                        ob.Count,
                        _context.products.FirstOrDefault(prod => prod.ProductId == ob.ProductId).NewPrice,
                        _context.products.FirstOrDefault(prod => prod.ProductId == ob.ProductId).Price
                    }),
                    Total = order.Products.Select(ob => new
                    {
                        _context.products.FirstOrDefault(prod => prod.ProductId == ob.ProductId).NewPrice,
                        _context.products.FirstOrDefault(prod => prod.ProductId == ob.ProductId).Price,
                        ob.Count
                    }).Sum(o => o.NewPrice == 0 ? o.Count * o.Price : o.Count * o.NewPrice)
                }));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Post([FromBody] Order orderDto)
        {
            IdentityUser user = userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
            
            var orderId = Guid.NewGuid();
            foreach (var product in orderDto.Products)
            {
                var prod = _context.products.FirstOrDefault(ob => ob.ProductId == product.ProductId);
                
                if (prod is null)
                {
                    return NotFound(new { Message ="Product hasn't been found."});
                }
                
                if (prod.Count < product.Count)
                {
                    return BadRequest(new { Message = "Out of stock." });
                }

                product.OrderId = orderId;
                product.OrderFormId = Guid.NewGuid();
            }
            
            orderDto.CreatedAt = DateTime.Now;
            orderDto.UserId = user.Id;
            
            _context.orders.Add(orderDto);
            _context.SaveChanges();
            
            return Ok(orderDto);
        }
    }
}