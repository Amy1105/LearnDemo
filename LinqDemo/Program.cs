using LinqDemo.Models;
using System.Xml.Linq;

//筛选  where

//投影 select  selectMany  Zip

//set运算 Distinct DistinctBy  Except  ExceptBy  Intersect  IntersectBy  Union  UnionBy

//对数据排序 OrderBy  OrderByDescending  ThenBy  ThenByDescending   Reverse

//限定运算符  all any contains

//数据分区 take  takeWhile   skip   skipWhile  Chunk

//转换数据类型

//联接操作 Join   GroupJoin

//分组 GroupBy  ToLookUp

string aString = "ABCDE99F-J74-12-89A";

//Char.IsDigit指示指定的Unicode字符是否分类为十进制数字。
var stringQuery = from ch in aString
                  where Char.IsDigit(ch)
                  select ch;

// Execute the query
foreach (char c in stringQuery)
    Console.Write(c + " ");

// Call the Count method on the existing query.
int count = stringQuery.Count();
Console.WriteLine($"Count = {count}");

Console.WriteLine($"aString = {aString}");
// Select all characters before the first '-'
var stringQuery2 = aString.TakeWhile(c => c != '-');

// Execute the second query
foreach (char c in stringQuery2)
    Console.Write(c);
/* Output:
  Output: 9 9 7 4 1 2 8 9
  Count = 8
  ABCDE99F
*/


//{
//    // ToList and ToArray cause the entire resultset to be buffered:
//    var blogsList = context.Posts.Where(p => p.Title.StartsWith("A")).ToList();
//    var blogsArray = context.Posts.Where(p => p.Title.StartsWith("A")).ToArray();

//    // Foreach streams, processing one row at a time:
//    foreach (var blog in context.Posts.Where(p => p.Title.StartsWith("A")))
//    {
//        // ...
//    }

//    // AsEnumerable也是流，允许您在客户端执行LINQ运算符：
//    var doubleFilteredBlogs = context.Posts
//        .Where(p => p.Title.StartsWith("A")) // 转换为SQL并在数据库中执行
//        .AsEnumerable()
//        .Where(p => SomeDotNetMethod(p)); // 在客户端对所有数据库结果执行
//}


{



    Pet pet1 = new Pet() { Name = "Turbo", Age = 2 };
    Pet pet2 = new Pet() { Name = "Peanut", Age = 8 };

    // Create two lists of pets.
    List<Pet> pets1 = new List<Pet> { pet1, pet2 };
    List<Pet> pets2 =
        new List<Pet> { new Pet { Name = "Turbo", Age = 2 },
                        new Pet { Name = "Peanut", Age = 8 } };

    bool equal = pets1.SequenceEqual(pets2);

    Console.WriteLine("The lists {0} equal.", equal ? "are" : "are not");


/*
 This code produces the following output:

 The lists are not equal.
*/
}

{

    // Create two arrays.
    string[] fruits1 = { "orange" };
    string[] fruits2 = { "orange", "apple" };

    // Get the only item in the first array.
    string fruit1 = fruits1.AsQueryable().Single();

    Console.WriteLine("First query: " + fruit1);

    try
    {
        // Try to get the only item in the second array.
        string fruit2 = fruits2.AsQueryable().Single();
        Console.WriteLine("Second query: " + fruit2);
    }
    catch (System.InvalidOperationException)
    {
        Console.WriteLine(
            "Second query: The collection does not contain exactly one element."
            );
    }

    /*
        This code produces the following output:

        First query: orange
        Second query: The collection does not contain exactly one element
    */
}

Console.ReadKey();


class Pet
{
    public string Name { get; set; }
    public int Age { get; set; }
}





