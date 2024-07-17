using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;

namespace EFCoreDemo.Services
{
    public class stringBenchmark2
    {
        private const string message1 = "hello,world!";
        private const string message2 = "hello,world!";

        [Benchmark]
        public bool Equals() => message1.Equals(message2,StringComparison.OrdinalIgnoreCase);

        [Benchmark]
        public bool Compare() => string.Compare(message1, message2, true)==0;

        [Benchmark]
        public bool ToLower() => message1.ToLower() == message2.ToLower();        
    }
}
