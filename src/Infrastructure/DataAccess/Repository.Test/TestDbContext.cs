using Infrastructure.Repository.Contracts.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Contracts.Test
{
    public class TestDbContext : BaseContext
    {
        public TestDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}