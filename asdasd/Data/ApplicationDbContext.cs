using Microsoft.EntityFrameworkCore;
using asdasd.Models;

namespace asdasd.Data;
    
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    public DbSet<Product> Products { get; set; }
}