using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using net9Demo;
using net9Demo.TextJsonDemo;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;




/*
1.ConcurrentBag:用于存储多线程处理的结果。ConcurrentBag 是线程安全的集合，适合在多线程环境中使用。

2.Parallel.ForEach:使用 Parallel.ForEach 并行处理文件读取和搜索操作，充分利用多核 CPU 的性能。

3.去重:使用 Distinct() 方法对结果去重。由于 ConcurrentBag 可能包含重复项（例如同一行被多次匹配），去重是必要的。

4.排序:使用 OrderBy 和 ThenBy 对结果按文件名和行号排序，使输出更有序。

5.异常处理:在文件读取过程中捕获异常，避免程序因单个文件读取失败而崩溃。
 * 
 */

// 指定要搜索的目录
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
//builder.Configuration.AddCommandLine(args);
//builder.Configuration.AddEnvironmentVariables(prefix: "PREFIX_");
//builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
//builder.Configuration.AddJsonFile("appsetting.json", optional: true,reloadOnChange:true);

//appsetting.json 取不到值的原因，appsetting.json 属性需要设置成始终复制



ConnectionStringsOption connectionStringsOption = new();
builder.Configuration.GetSection(nameof(ConnectionStringsOption))
    .Bind(connectionStringsOption);


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{

    //var person = new Person { Name = "Alice", Age = 30, BirthDate = new DateTime(1993, 5, 10) };
    //string json = JsonSerializer.Serialize(person);
    //Console.WriteLine(json);
    //// 输出: {"Name":"Alice","Age":30,"BirthDate":"1993-05-10T00:00:00"}

    //// 反序列化 JSON → 对象
    //string jsonInput = "{\"Name\":\"Bob\",\"Age\":25,\"BirthDate\":\"1998-03-15\"}";
    //Person deserializedPerson = JsonSerializer.Deserialize<Person>(jsonInput);
    //Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
    //// 输出: Name: Bob, Age: 25


  //  SerializeBasic.Test();
    DeserializeExtra.Test();


    //var options = new JsonSerializerOptions
    //{
    //    WriteIndented = true,                // 格式化输出（缩进）
    //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // 属性名转为驼峰命名
    //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // 忽略null值
    //    Converters = { new JsonStringEnumConverter() }     // 枚举转字符串
    //};
    //var data = new { Name = "Charlie", Score = 95, Level = Level.Advanced };
    //string json2 = JsonSerializer.Serialize(data, options);
    //Console.WriteLine(json2);

}