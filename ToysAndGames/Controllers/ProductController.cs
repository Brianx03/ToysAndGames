using Microsoft.AspNetCore.Mvc;
using ToysAndGames.Services;
using ToysAndGamesModel.Models;

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

        [HttpGet]
        public IActionResult GetProduct()
        {
            var products = _productServices.Get();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if(product == null)
                return BadRequest();
            try
            {
                var newProduct = _productServices.Insert(product);
                return Created("api/product", newProduct);
            }
            catch(Exception e)
            {
                return (IActionResult)e;
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            var productUpdated = _productServices.Update(product);
            return Ok(productUpdated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            if (_productServices.Delete(id) == 0) 
            {
                return NotFound();
            }
            return NoContent();      
        }
    }
}
