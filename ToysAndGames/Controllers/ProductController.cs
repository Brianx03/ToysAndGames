using Microsoft.AspNetCore.Mvc;
using ToysAndGames_DataAccess.Data;
using ToysAndGames_Model.Models;

namespace ToysAndGames.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct()
        {
            List<Product> objList = _db.Products.ToList();
            return Ok(objList);
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
            }
            return Ok();
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            Product obj = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            obj.Name = product.Name;
            obj.Description = product.Description;
            obj.AgeRestriction = product.AgeRestriction;
            obj.Company = product.Company;
            obj.Price = product.Price;

            if (ModelState.IsValid)
            {
                _db.Products.Update(obj);
                _db.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            Product? obj = _db.Products.FirstOrDefault(p => p.Id == id);

            if (ModelState.IsValid)
            {
                _db.Products.Remove(obj);
                _db.SaveChanges();
            }
            return Ok();
        }
    }
}
