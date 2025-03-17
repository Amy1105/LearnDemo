using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchString.ConcurrentFiles
{
    // 要存储在字典中的值的类型。
    class CityInfo : IEqualityComparer<CityInfo>
    {
        public string Name { get; set; }
        public DateTime LastQueryDate { get; set; } = DateTime.Now;
        public decimal Longitude { get; set; } = decimal.MaxValue;
        public decimal Latitude { get; set; } = decimal.MaxValue;
        public int[] RecentHighTemperatures { get; set; } = new int[] { 0 };

        public bool Equals(CityInfo x, CityInfo y)
            => (x.Name, x.Longitude, x.Latitude) ==
                  (y.Name, y.Longitude, y.Latitude);

        public int GetHashCode(CityInfo cityInfo) =>
            cityInfo?.Name.GetHashCode() ?? throw new ArgumentNullException(nameof(cityInfo));
    }

    internal class TestConcurrentDictionary
    {
        static readonly ConcurrentDictionary<string, CityInfo> Cities =
            new ConcurrentDictionary<string, CityInfo>(StringComparer.OrdinalIgnoreCase);

        public static async Task Method()
        {
            CityInfo[] cityData =
            {
                new CityInfo { Name = "Boston", Latitude = 42.358769m, Longitude = -71.057806m, RecentHighTemperatures = new int[] { 56, 51, 52, 58, 65, 56,53} },
                new CityInfo { Name = "Miami", Latitude = 25.780833m, Longitude = -80.195556m, RecentHighTemperatures = new int[] { 86,87,88,87,85,85,86 } },
                new CityInfo { Name = "Los Angeles", Latitude = 34.05m, Longitude = -118.25m, RecentHighTemperatures =   new int[] { 67,68,69,73,79,78,78 } },
                new CityInfo { Name = "Seattle", Latitude = 47.609722m, Longitude =  -122.333056m, RecentHighTemperatures =   new int[] { 49,50,53,47,52,52,51 } },
                new CityInfo { Name = "Toronto", Latitude = 43.716589m, Longitude = -79.340686m, RecentHighTemperatures =   new int[] { 53,57, 51,52,56,55,50 } },
                new CityInfo { Name = "Mexico City", Latitude = 19.432736m, Longitude = -99.133253m, RecentHighTemperatures =   new int[] { 72,68,73,77,76,74,73 } },
                new CityInfo { Name = "Rio de Janeiro", Latitude = -22.908333m, Longitude = -43.196389m, RecentHighTemperatures =   new int[] { 72,68,73,82,84,78,84 } },
                new CityInfo { Name = "Quito", Latitude = -0.25m, Longitude = -78.583333m, RecentHighTemperatures =   new int[] { 71,69,70,66,65,64,61 } },
                new CityInfo { Name = "Milwaukee", Latitude = -43.04181m, Longitude = -87.90684m, RecentHighTemperatures =   new int[] { 32,47,52,64,49,44,56 } }
            };

            // 从多个线程中添加一些键/值对。
            await Task.WhenAll(
                Task.Run(() => TryAddCities(cityData)),
                Task.Run(() => TryAddCities(cityData)));

            static void TryAddCities(CityInfo[] cities)
            {
                for (var i = 0; i < cities.Length; ++i)
                {
                    var (city, threadId) = (cities[i], Thread.CurrentThread.ManagedThreadId);
                    if (Cities.TryAdd(city.Name, city))
                    {
                        Console.WriteLine($"Thread={threadId}, added {city.Name}.");
                    }
                    else
                    {
                        Console.WriteLine($"Thread={threadId}, could not add {city.Name}, it was already added.");
                    }
                }
            }

            // 从应用程序主线程枚举集合。
            //请注意，ConcurrentDictionary是一个不支持线程安全枚举的并发集合。
            foreach (var city in Cities)
            {
                Console.WriteLine($"{city.Key} has been added.");
            }

            AddOrUpdateWithoutRetrieving();
            TryRemoveCity();
            RetrieveValueOrAdd();
            RetrieveAndUpdateOrAdd();

            Console.WriteLine("Press any key.");
            Console.ReadKey();
        }

        // 此方法显示了如何在键可能已经存在的情况下将键值对添加到字典中。
        static void AddOrUpdateWithoutRetrieving()
        {
            // 稍后某个时间。我们从某个来源获得了新的数据。
            var ci = new CityInfo
            {
                Name = "Toronto",
                Latitude = 43.716589M,
                Longitude = -79.340686M,
                RecentHighTemperatures = new int[] { 54, 59, 67, 82, 87, 55, -14 }
            };

            // 尝试添加数据。如果它不存在，则添加对象ci。
            // 如果它已经存在，请根据自定义逻辑更新existingVal。
            _ = Cities.AddOrUpdate(
                ci.Name,
                ci,
                (cityName, existingCity) =>
                {
                    //如果调用此委托，则密钥已存在。
                    // 在这里，我们确保这座城市真的是我们已经拥有的同一座城市。
                    if (ci != existingCity)
                    {
                        // throw new ArgumentException($"Duplicate city names are not allowed: {ci.Name}.");
                    }

                    // 唯一可更新的字段是温度数组和LastQueryDate.
                    existingCity.LastQueryDate = DateTime.Now;
                    existingCity.RecentHighTemperatures = ci.RecentHighTemperatures;

                    return existingCity;
                });

            // 验证字典是否包含新的或更新的数据。
            Console.Write($"Most recent high temperatures for {ci.Name} are: ");
            var temps = Cities[ci.Name].RecentHighTemperatures;
            Console.WriteLine(string.Join(", ", temps));
        }

        // 此方法显示了如何使用数据并确保已将其添加到字典中。
        static void RetrieveValueOrAdd()
        {
            var searchKey = "Caracas";
            CityInfo retrievedValue = null;

            try
            {
                retrievedValue = Cities.GetOrAdd(searchKey, GetDataForCity(searchKey));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            // Use the data.
            if (retrievedValue != null)
            {
                Console.Write($"Most recent high temperatures for {retrievedValue.Name} are: ");
                var temps = Cities[retrievedValue.Name].RecentHighTemperatures;
                Console.WriteLine(string.Join(", ", temps));
            }
        }

        //此方法显示如何从字典中删除值。
        // 如果无法删除该值，则可以通过使用返回的布尔值来处理。尝试删除功能。
        static void TryRemoveCity()
        {
            Console.WriteLine($"Total cities = {Cities.Count}");

            var searchKey = "Milwaukee";
            if (Cities.TryRemove(searchKey, out CityInfo retrievedValue))
            {
                Console.Write($"Most recent high temperatures for {retrievedValue.Name} are: ");
                var temps = retrievedValue.RecentHighTemperatures;
                Console.WriteLine(string.Join(", ", temps));
            }
            else
            {
                Console.WriteLine($"Unable to remove {searchKey}");
            }

            Console.WriteLine($"Total cities = {Cities.Count}");
        }

        // 此方法显示了当您预期键/值对已经存在时，如何从字典中检索值，然后可能用键的新值更新字典。
        static void RetrieveAndUpdateOrAdd()
        {
            var searchKey = "Buenos Aires";
            if (Cities.TryGetValue(searchKey, out CityInfo retrievedValue))
            {
                // Use the data.
                Console.Write($"Most recent high temperatures for {retrievedValue.Name} are: ");
                var temps = retrievedValue.RecentHighTemperatures;
                Console.WriteLine(string.Join(", ", temps));

                // 复制数据。我们的对象将自动更新其LastQueryDate。
                var newValue = new CityInfo
                {
                    Name = retrievedValue.Name,
                    Latitude = retrievedValue.Latitude,
                    Longitude = retrievedValue.Longitude,
                    RecentHighTemperatures = retrievedValue.RecentHighTemperatures
                };

                // 用新值替换旧值。
                if (!Cities.TryUpdate(searchKey, newValue, retrievedValue))
                {
                    // 数据未更新。日志错误、抛出异常等。
                    Console.WriteLine($"Could not update {retrievedValue.Name}");
                }
            }
            else
            {
                //添加新的键和值。在这里，我们调用一个方法来检索数据。另一种选择是在此处添加默认值，然后在其他线程上使用真实数据进行更新。
                var newValue = GetDataForCity(searchKey);
                if (Cities.TryAdd(searchKey, newValue))
                {
                    // Use the data.
                    Console.Write($"Most recent high temperatures for {newValue.Name} are: ");
                    var temps = newValue.RecentHighTemperatures;
                    Console.WriteLine(string.Join(", ", temps));
                }
                else
                {
                    Console.WriteLine($"Unable to add data for {searchKey}");
                }
            }
        }

        // 假设此方法知道如何查找任何指定城市的long/lat/temp信息。
        static CityInfo GetDataForCity(string name) => name switch
        {
            "Caracas" => new CityInfo
            {
                Name = "Caracas",
                Longitude = 10.5M,
                Latitude = -66.916667M,
                RecentHighTemperatures = new int[] { 91, 89, 91, 91, 87, 90, 91 }
            },
            "Buenos Aires" => new CityInfo
            {
                Name = "Buenos Aires",
                Longitude = -34.61M,
                Latitude = -58.369997M,
                RecentHighTemperatures = new int[] { 80, 86, 89, 91, 84, 86, 88 }
            },
            _ => throw new ArgumentException($"Cannot find any data for {name}")
        };
    }
}