using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Infrastructure.Repository.Contracts.Test
{
    public abstract class TestBase
    {
        public Container Container { get; set; }

        public void InitContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<BaseContext>(() =>
            {
                var builder = new DbContextOptionsBuilder();
                builder.UseInMemoryDatabase("Add_writes_to_database");
                return new TestDbContext(builder.Options);
            }, Lifestyle.Scoped);
            container.Register<IRepository, Repository>();
            container.Verify();
            Container = container;
        }
    }
}