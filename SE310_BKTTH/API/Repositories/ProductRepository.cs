using API.Data;
using API.Interfaces;
using API.Models;
using API.Models.Entity;

namespace API.Repositories;

public class ProductRepository :IProductRepository
{
    private readonly DataContext _context;
    
    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.OrderBy(p => p.Id).ToList();
    }

    public Product GetProduct(int id)
    {
        return _context.Products.Where(p => p.Id == id).FirstOrDefault();
    }

    public Product AddProduct(AddProductDto addProductDto)
    {
        var product = new Product
        {
            Name = addProductDto.Name,
            Description = addProductDto.Description,
            ImageUrl = addProductDto.ImageUrl,
            Price = addProductDto.Price
        };
        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product UpdateProduct(int id, UpdateProductDto updateProductDto)
    {
        var product = _context.Products.Find(id);
        if (product is null)
        {
            return null;
        }
        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.ImageUrl = updateProductDto.ImageUrl;
            
        _context.SaveChanges();
        return product;
    }

    public bool DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
