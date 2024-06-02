using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using EFCore.BulkExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Services.Example
{
    //[SimpleJob(RuntimeMoniker.Net80, warmupCount: 5, iterationCount: 1)]
    //public class PerformanceBenchmark
    //{              
    //    [GlobalSetup]
    //    public void Setup()
    //    {
                      
    //    }
     
    //    [Params(1)]
    //    public int N;

    //    [Benchmark]
    //    public void WithoutOrderBy()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var query1 = context.Blogs.AsQueryable();
    //            var query2 = context.Posts.Include(a => a.Tags).ThenInclude(b => b.Tag);
    //            var query = from a in query1
    //                        join b in query2 on a.BlogId equals b.BlogId into go
    //                        from b in go.DefaultIfEmpty()
    //                        select new
    //                        {
    //                            a,
    //                            b,
    //                            b.Tags,
    //                        };
    //            var list = query.Skip(2100).Take(1000).ToList();
    //            Console.WriteLine(list.Count());
    //        }
    //    }

    //    [Benchmark]
    //    public void WithOrderBy()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var query1 = context.Blogs.AsQueryable();
    //            var query2 = context.Posts.Include(a => a.Tags).ThenInclude(b => b.Tag);
    //            var query = from a in query1
    //                        join b in query2 on a.BlogId equals b.BlogId into go
    //                        from b in go.DefaultIfEmpty()
    //                        select new
    //                        {
    //                            a,
    //                            b,
    //                            b.Tags,
    //                        };
    //            var list = query.OrderBy(a => a.a.BlogId).Skip(2100).Take(1000).ToList();
    //            Console.WriteLine(list.Count());
    //        }
    //    }
    //}
}