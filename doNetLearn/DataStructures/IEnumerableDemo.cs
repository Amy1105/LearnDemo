using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DataStructures
{
    public class IEnumerableDemo
    {
        public static void GroupByDemo()
        {
            var items = new List<Item>
        {
            new Item { Category = "Book", SubCategory = "Fiction", Name = "Book1" },
            new Item { Category = "Book", SubCategory = "Non-Fiction", Name = "Book2" },
            new Item { Category = "Game", SubCategory = "Action", Name = "Game1" },
            new Item { Category = "Game", SubCategory = "Strategy", Name = "Game2" }
        };

            var groupedItems = items.GroupBy(item => new { item.Category, item.SubCategory })
                                    .Select(group => new
                                    {
                                        Category = group.Key.Category,
                                        SubCategory = group.Key.SubCategory,
                                        Items = group.Select(item => item.Name).ToList()
                                    });

            foreach (var group in groupedItems)
            {
                Console.WriteLine($"Category: {group.Category}, SubCategory: {group.SubCategory}");
                foreach (var name in group.Items)
                {
                    Console.WriteLine($"- {name}");
                }
            }
        }
    }


    public class Item
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Name { get; set; }
    }
}
