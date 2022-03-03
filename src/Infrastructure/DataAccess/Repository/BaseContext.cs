using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}