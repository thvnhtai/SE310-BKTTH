using Microsoft.EntityFrameworkCore;
using SE310_BKTTH.Models;

namespace SE310_BKTTH.Data;

public class DataContext : DbContext
{
    public DataContext(){}
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<ProductModel> Products { get; set; }
}
