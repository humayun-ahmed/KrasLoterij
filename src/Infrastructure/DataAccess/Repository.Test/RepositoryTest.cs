using Infrastructure.Repository.Contracts.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector.Lifestyles;

namespace Infrastructure.Repository.Contracts.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class RepositoryTest : TestBase
    {
        public IRepository Repository { get; set; }

        [TestInitialize]
        public void Init()
        {
            InitContainer();
        }

        [TestMethod]
        public void AddMustSucceed()
        {
            using (AsyncScopedLifestyle.BeginScope(Container))
            {
                Repository = Container.GetInstance<IRepository>();
                var addedItem = Add();
                var categories = Repository.GetItems<ProductCategory>(p => p.Id == addedItem.Id);
                Assert.AreEqual(1, categories.Count());
            }
        }

        [TestMethod]
        public void UpdateSucceed()
        {
            using (AsyncScopedLifestyle.BeginScope(Container))
            {
                Repository = Container.GetInstance<IRepository>();
                var addedItem = Add();
                addedItem.CategoryName = "Bat";
                Repository.Update(addedItem);
                Repository.SaveChanges();
                var categories = Repository.GetItems<ProductCategory>(p => p.CategoryName == "Bat");
                Assert.AreEqual(1, categories.Count());
            }
        }

        [TestMethod]
        public void DeleteMustSucceed()
        {
            using (AsyncScopedLifestyle.BeginScope(Container))
            {
                Repository = Container.GetInstance<IRepository>();
                var addedItem = Add();
                Repository.Remove<ProductCategory>(p => p.Id == addedItem.Id);
                Repository.SaveChanges();
                var categories = Repository.GetItems<ProductCategory>(p => p.Id == addedItem.Id);
                Assert.AreEqual(0, categories.Count());
            }
        }


        [TestMethod]
        public async Task GetItemMustSucceed()
        {
            using (AsyncScopedLifestyle.BeginScope(Container))
            {
                Repository = Container.GetInstance<IRepository>();
                var addedItem = Add();
                var item = await Repository.GetItem<ProductCategory>(p => p.Id == addedItem.Id);
                Assert.AreEqual(addedItem.Id, item.Id);
            }
        }

        private ProductCategory Add()
        {
            var category = new ProductCategory
            {
                CategoryCode = "2363",
                CategoryName = "BALL",
                Id = Guid.NewGuid()
            };
            Repository.Add(category);
            Repository.SaveChanges();
            return category;
        }
    }
}