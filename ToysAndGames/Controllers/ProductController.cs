using Microsoft.AspNetCore.Mvc;
using ToysAndGames.Services;
using ToysAndGamesModel.Models;

namespace ToysAndGames.Controllers
{

    //TODO: Move the services to a different Layer


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
        public async Task<ActionResult> GetProduct()
        {
            var products = await _productServices.Get();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var newProduct = await _productServices.Insert(product);
            return Created("api/product", newProduct);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            var productUpdated = await _productServices.Update(product);
            return Ok(productUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            if (await _productServices.Delete(id) == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
