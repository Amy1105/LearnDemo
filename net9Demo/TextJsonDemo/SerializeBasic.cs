using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace net9Demo.TextJsonDemo
{
    internal class SerializeBasic
    {
        public static void Test()
        {
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot",
                TemperatureRanges = new Dictionary<string, int>()
                {
                    { "ColdMinTemp",20 },
                    { "HotMinTemp", 40 },
                },
                SummaryEnum = Summary.Warm,
                Precipitation= Precipitation.Snow,
                Sky = CloudCover.Partial,
                Password="123456"
            };

            var options = new JsonSerializerOptions
            {
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,//使用内置命名策略
                //DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, //对字典键使用命名策略,字典键的命名策略仅适用于序列化
                //Converters =
                //{   //序列化枚举名称(而不是数值)，并将名称转换为 camel 大小写,内置 JsonStringEnumConverter 还可以反序列化字符串值
                //    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                //},
                WriteIndented = true,
               // IgnoreReadOnlyProperties = true,//忽略所有只读属性，
               DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault //忽略所有，根据具体属性含义

            }; //进行优质打印
            string jsonString = JsonSerializer.Serialize(weatherForecast, options);
            Console.WriteLine(jsonString);
        }
    }


    public class WeatherForecast
    {
        [JsonPropertyOrder(-5)]  //配置序列化属性的顺序 , 属性按 Order 值从小到大的顺序编写的
        public DateTimeOffset Date { get; set; }
        [JsonPropertyOrder(-2)]
        public int TemperatureCelsius { get; set; }
        [JsonPropertyOrder(5)]
        public string? Summary { get; set; }

        [JsonPropertyName("Wind")]  //自定义单个属性名称,1.同时适用于两个方向（序列化和反序列化）。
        //2.优先于属性命名策略。
//3.不影响参数化构造函数的参数名称匹配。
        public int WindSpeed { get; set; }

        public Dictionary<string,int> TemperatureRanges { get; set; }

        public Summary? SummaryEnum { get; set; }

        public Precipitation? Precipitation { get; set; }
  
        public CloudCover? Sky { get; set; }

        [JsonIgnore]  //忽略单个属性
        public string Password {  get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]//如何为null就忽略
        public string Habby { get; set; }

        public int WindSpeedReadOnly { get; private set; } = 35;

        public string Description { get;  set; } = "描述：";
    }

    public enum Summary
    {
        Cold, Cool, Warm, Hot
    }

    /// <summary>
    /// Json转换器属性,批注枚举来指定要使用的转换器
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<Precipitation>))]
    public enum Precipitation
    {
        Drizzle, Rain, Sleet, Hail, Snow
    }

    /// <summary>
    /// .NET 9 开始，可以为序列化为字符串的类型自定义单个枚举成员的名称
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CloudCover
    {
        Clear,
        [JsonStringEnumMemberName("Partly cloudy")]
        Partial,
        Overcast
    }

}
