using System.Collections;
using API.Models;
using API.Models.Entity;

namespace API.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();
    Product GetProduct(int id);
    Product AddProduct(AddProductDto addProductDto);
    Product UpdateProduct(int id, UpdateProductDto updateProductDto);
    bool DeleteProduct(int id);
}
