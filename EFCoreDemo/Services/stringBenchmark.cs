using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;

namespace EFCoreDemo.Services
{
    public class stringBenchmark
    {
        private const string testString = "hello,world!";
        private const char testChar = 'd';

        [Benchmark]
        public bool IndexOf() => testString.IndexOf(testChar) >= 0;

        [Benchmark]
       public bool Contains()=>testString.Contains(testChar);

        [Benchmark]
        public bool Any()=>testString.Any(c=>c==testChar);
    }
}
