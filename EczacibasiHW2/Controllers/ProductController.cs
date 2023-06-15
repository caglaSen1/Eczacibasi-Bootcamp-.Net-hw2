using EczacibasiHW2.Models;
using EczacibasiHW2.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace EczacibasiHW2.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            _productRepository.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetAll(int page, int pageSize)
        {
            return Ok(_productRepository.GetAll(page, pageSize));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            _productRepository.Update(id, product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult Search(string name, int? categoryId, double? minPrice)
        {
            return Ok(_productRepository.Search(name, categoryId, minPrice));
        }      
        
    }
}
