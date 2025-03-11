using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.ThreadDemo
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
    internal class ReadCSV
    {
        /// <summary>
        /// 原方法
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="categoryFilter"></param>
        public void ProcessProducts(string filePath, string categoryFilter)
        {
            var products = new List<Product>();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var values = line.Split(',');

                    var product = new Product
                    {
                        Name = values[0],
                        Category = values[1],
                        Price = decimal.Parse(values[2])
                    };
                    products.Add(product);
                }
            }

            var filteredProducts = products.Where(p => p.Category == categoryFilter);

            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"{product.Name}, {product.Price:C}");
            }
        }

        /// <summary>
        /// 职责分离
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IEnumerable<Product> LoadProducts(string filePath)
        {
            var products = new List<Product>();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var values = line.Split(',');

                    var product = new Product
                    {
                        Name = values[0],
                        Category = values[1],
                        Price = decimal.Parse(values[2])
                    };
                    products.Add(product);
                }
            }
            return products;
        }

        /// <summary>
        /// 考虑异常
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IEnumerable<Product> LoadProducts2(string filePath)
        {
            var products = new List<Product>();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var values = line.Split(',');
                    try
                    {
                        var product = new Product
                        {
                            Name = values[0],
                            Category = values[1],
                            Price = decimal.Parse(values[2])
                        };
                        products.Add(product);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error parsing product data: {ex.Message}");
                    }
                }
            }

            return products;
        }

        public IEnumerable<Product> FilterProductsByCategory(IEnumerable<Product> products, string category)
        {
            return products.Where(p => p.Category == category);
        }

        public void DisplayProducts(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name}, {product.Price:C}");
            }
        }


    }
    /// <summary>
    /// 使用设计模式 Strategy Pattern
    /// </summary>
    public interface IProductFilterStrategy
    {
        IEnumerable<Product> Filter(IEnumerable<Product> products);
    }

    public class CategoryFilter : IProductFilterStrategy
    {
        private readonly string _category;

        public CategoryFilter(string category)
        {
            _category = category;
        }

        public IEnumerable<Product> Filter(IEnumerable<Product> products)
        {
            return products.Where(p => p.Category == _category);
        }
    }

    public class PriceFilter : IProductFilterStrategy
    {
        private readonly decimal _minPrice;
        private readonly decimal _maxPrice;

        public PriceFilter(decimal minPrice, decimal maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        public IEnumerable<Product> Filter(IEnumerable<Product> products)
        {
            return products.Where(p => p.Price >= _minPrice && p.Price <= _maxPrice);
        }
    }


    //最后，测试驱动开发

}
