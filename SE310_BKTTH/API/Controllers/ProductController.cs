using API.Models;
using API.Models.Entity;
using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.GetProduct(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
           var product = _productRepository.AddProduct(addProductDto);
           return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
           var updatedProduct = _productRepository.UpdateProduct(id, updateProductDto);
           if (updatedProduct == null) return NotFound();

           return Ok(updatedProduct);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var isDeleted = _productRepository.DeleteProduct(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent(); // Typically returns 204 No Content for successful deletes
        }


    }
}
