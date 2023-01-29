using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduces()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
            {
                _logger.LogError($"Product with id: {id},not found");
                return NotFound();
            }
            return Ok(product);
        }


        [HttpGet]
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            var products = await _repository.GetProductByCategory(category);           
            return Ok(products);
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var products = await _repository.GetProductByName(name);
            if (products == null)
            {
                _logger.LogError($"Product with name: {name},not found");
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            return Ok(await _repository.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}",Name = "Delete")]        
        public async Task<IActionResult> Delete(string id) 
        {
            return Ok(await _repository.DeleteProduct(id));
        }

    }
}
