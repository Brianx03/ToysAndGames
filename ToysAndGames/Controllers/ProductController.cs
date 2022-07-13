using Microsoft.AspNetCore.Mvc;
using ToysAndGames.Services;
using ToysAndGames_DataAccess.Data;
using ToysAndGames_Model.Models;

namespace ToysAndGames.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct()
        {
            var products = _productServices.Get();
            return Ok(products);
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            else
            {
                var newProduct = _productServices.Insert(product);
                return Ok(newProduct);
            }
        }

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            else
            {
                var productUpdated = _productServices.Update(product);
                return Ok(productUpdated);
            }
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                _productServices.Delete(id);
                return NoContent();
            }
        }
    }
}
