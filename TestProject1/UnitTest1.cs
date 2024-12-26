using doNetLearn;
using System.Diagnostics;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestFixture]
        public class ProductFilterTests
        {
            [Test]
            public void CategoryFilter_ShouldReturnOnlyMatchingProducts()
            {
                var products = new List<Product>
                {
                    new Product { Name = "Laptop", Category = "Electronics", Price = 1000 },
                    new Product { Name = "Book", Category = "Books", Price = 20 }
                };
                var categoryFilter = new CategoryFilter("Electronics");
                var filteredProducts = categoryFilter.Filter(products);
                Assert.AreEqual(1, filteredProducts.Count());
                Assert.AreEqual("Laptop", filteredProducts.First().Name);
            }

            [Test]
            public void PriceFilter_ShouldReturnProductsWithinPriceRange()
            {
                var products = new List<Product>
                {
                    new Product { Name = "Laptop", Category = "Electronics", Price = 1000 },
                    new Product { Name = "Book", Category = "Books", Price = 20 }
                };
                var priceFilter = new PriceFilter(50, 2000);
                var filteredProducts = priceFilter.Filter(products);
                Assert.AreEqual(1, filteredProducts.Count());
                Assert.AreEqual("Laptop", filteredProducts.First().Name);
            }
        }
    }
}