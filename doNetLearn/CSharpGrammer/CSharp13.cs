﻿using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    /// <summary>
    /// 作用：是C#中的一项重要特性，它允许开发人员向方法传递数量可变的参数，这些参数会自动封装到一个数组中。
    /// 在C# 13之前，params关键字仅限于数组使用。然而，在C#的最新版本中，现在可以将params与其他集合类型一起使用了。
    /// 
    ///  C# 13中的这一变化扩展了params的功能，使其能够与ReadOnlySpan<T>和List<T>等类型一起使用。
    ///  这些类型具有更好的性能特性，例如避免不必要的堆分配，并能实现高效的切片和访问，尤其是ReadOnlySpan<T>。
    /// </summary>
    internal class CSharp13paramsDemo
    {      
        /*
与ReadOnlySpan<T>一起使用params ReadOnlySpan<int>是C#中的一种类型，它表示内存中连续的只读区域，可用于查看和操作数组或内存块。它为传统数组提供了一种更安全、更高效的替代方案，特别是当你想要访问数据而不修改它时
ReadOnlySpan<T>的关键特性：
内存效率：ReadOnlySpan<T> 允许你操作数组或内存的切片而无需创建副本，使其成为处理大型数据集时更节省内存的方式。
无内存分配：它不会分配新内存。相反，它可用于操作现有内存，例如数组中的元素或更大数据结构的一部分。
只读：顾名思义，ReadOnlySpan<T> 是只读的，这意味着你不能修改它所指向的数据。这确保了数据完整性，避免了意外修改。
安全性：它提供边界检查，确保你不会访问有效范围之外的数据，防止越界错误。
无需垃圾回收：ReadOnlySpan<T> 可以指向栈分配的内存，不像数组是堆分配并由垃圾回收机制管理的。这可以带来性能提升，特别是对于生命周期较短的数据。
为什么对List<string> 使用params？
灵活性：通过对List<string> 使用params，方法可以接受任意数量的列表，并且每个列表可以包含不同数量的元素。这使得方法非常灵活且易于使用。
代码更简洁：无需手动构造列表数组或显式传入数组。可以直接传递List<string> 对象，甚至可以即时将列表与其他日志条目合并。
与集合协作：列表比数组更具动态性。使用List<T>，可以在创建后添加、删除或修改元素。对List<string> 使用params使我们能够充分利用集合的灵活性，同时仍保持处理多个参数的能力。
关键优势：
无需预定义数组：使用params List<string>[]，可以直接传递多个List<string> 参数，使代码更简单、更直观。
动态列表：List<string> 允许动态调整大小，将其作为params参数传递可以灵活地处理数量可变的日志条目。
        */
    }

    /// <summary>
    /// linq 引入了新方法：CountBy，AggregateBy，Index
    /// </summary>
    internal class CSharp13
    {
        public List<City> cities = new List<City>()
            {
                new City("Paris", "France"),
                new City("Berlin", "Germany"),
                new City("Madrid", "Spain"),
                new City("Rome", "Italy"),
                new City("Amsterdam", "Netherlands")
            };
        /// <summary>
        /// Index方法返回一个元组（IEnumerable<(int Index, TSource Item)>），其中第一个值是索引，第二个值是集合中的元素。
        /// </summary>
        public void Method()
        {
            //foreach ((int index, City city) in cities.Index()) //C# 13 = .net 9
            //{
            //    Console.WriteLine($"Index: {index}, City: {city.Name}");
            //}
        }

        public void Method2()
        {
            var citiesCount = cities.Count();
            for (int i = 0; i < citiesCount; i++)
            {
                Console.WriteLine($"Index: {i}, City: {cities[i].Name}");
            }
            var indexedElements = cities.Select((item, index) => new { Index = index, Item = item });
            foreach (var item in indexedElements)
            {
                Console.WriteLine($"Index: {item.Index}, City: {item.Item.Name}");
            }
        }
        //待基准测试 to do...


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ts"></param>
        //public void Method3(params ReadOnlySpan<T> ts)
        //{

        //    //现在可以将 params 用于任何已识别的集合类型，包括 System.Span < T >、System.ReadOnlySpan < T >，以及那些实现 System.Collections.Generic.IEnumerable<T> 并具有 Add 方法的类型。 除了具体类型外，还可以使用接口 System.Collections.Generic.IEnumerable < T >、System.Collections.Generic.IReadOnlyCollection < T >、System.Collections.Generic.IReadOnlyList < T >、System.Collections.Generic.ICollection<T> 和 System.Collections.Generic.IList < T >。
        //}
    }

    public class City
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public City(string name, string Country)
        {
            Name = name;
            Country = Country;
        }
    }
}
