using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using EFCore.BulkExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EFCoreDemo.Services.Example
{
    //[SimpleJob(RuntimeMoniker.Net80, warmupCount: 5, iterationCount: 5)]
    //public class NavigatorBenchmark
    //{       

    //    [GlobalSetup]
    //    public void Setup()
    //    {           
    //    }      

    //    //[Params(1, 3)] public int N;

    //    [Params(1)]
    //    public int N;

    //    [Benchmark]
    //    public void SingleInclude()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs.Include(blog => blog.Posts).ToList();
    //        }                
    //    }

    //    [Benchmark]
    //    public void SingleLINQ()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var query = from a in context.Blogs
    //                        join b in context.Posts on a.BlogId equals b.BlogId into g
    //                        from b in g.DefaultIfEmpty()
    //                        select new
    //                        {
    //                            a,
    //                            b
    //                        };
    //            var blogs = query.ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void MultipleInclude()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs
    //            .Include(blog => blog.Posts)
    //            .Include(blog => blog.Owner)
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void MultipleLINQ()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var query = from a in context.Blogs
    //                        join b in context.Posts on a.BlogId equals b.BlogId into g
    //                        from b in g.DefaultIfEmpty()
    //                        join c in context.Person on a.OwnerId equals c.PersonId into m
    //                        from c in m.DefaultIfEmpty()
    //                        select new
    //                        {
    //                            a,
    //                            b,
    //                            c
    //                        };
    //            var blogs = query.ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void SingleChildInclude()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs
    //            .Include(blog => blog.Posts)
    //            .ThenInclude(post => post.Author)
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void SingleChildLINQ()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var query0 =
    //            from post in context.Posts
    //            join person in context.Person on post.AuthorId equals person.PersonId into g
    //            from person in g.DefaultIfEmpty()
    //            select new
    //            {
    //                post,
    //                person
    //            };

    //            var query1 =
    //                from a in context.Posts
    //                join b in query0 on a.AuthorId equals b.post.BlogId into g
    //                from b in g.DefaultIfEmpty()
    //                select new
    //                {
    //                    a,
    //                    b.post,
    //                    b.person
    //                };

    //            var blogs = query1.ToList();
    //        }
    //    }


    //    [Benchmark]
    //    public void MultipleThenIncludes()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs
    //            .Include(blog => blog.Posts)
    //            .ThenInclude(post => post.Author)
    //            .ThenInclude(author => author.Photo)
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void IncludeTree()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs
    //            .Include(blog => blog.Posts)
    //            .ThenInclude(post => post.Author)
    //            .ThenInclude(author => author.Photo)
    //            .Include(blog => blog.Owner)
    //            .ThenInclude(owner => owner.Photo)
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void MultipleLeafIncludes()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs
    //            .Include(blog => blog.Posts)
    //            .ThenInclude(post => post.Author)
    //            .Include(blog => blog.Posts)
    //            .ThenInclude(post => post.Tags)
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void IncludeMultipleNavigationsWithSingleInclude()
    //    {
    //        using (var context = Helper.GetContext())
    //        {             
    //            var blogs = context.Blogs
    //                .Include(blog => blog.Owner.AuthoredPosts)
    //                .ThenInclude(post => post.Blog.Owner.Photo)
    //                .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void MultipleLeafIncludesFiltered2()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var filteredBlogs = context.Blogs
    //            .Include(blog => blog.Posts.Where(post => post.BlogId == 1))
    //            .ThenInclude(post => post.Author)
    //            .Include(blog => blog.Posts.Where(post => post.BlogId == 1))
    //            .ThenInclude(post => post.Tags.OrderBy(postTag => postTag.TagId).Skip(3))
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void LeftOuterJoin()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs
    //            .Include(blog => blog.Posts)
    //            .ThenInclude(post => post.Tags)
    //            .ThenInclude(tags => tags.Tag)
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void LeftOuterJoinOrderBy()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var blogs = context.Blogs
    //            .Include(blog => blog.Posts)
    //            .ThenInclude(post => post.Tags)
    //            .ThenInclude(tags => tags.Tag)
    //            .OrderBy(a => a.BlogId)
    //            .ToList();
    //        }
    //    }

    //    [Benchmark]
    //    public void LeftOuterJoinWithLinQ()
    //    {
    //        using (var context = Helper.GetContext())
    //        {
    //            var query = from blog in context.Blogs
    //                        join post in context.Posts on blog.BlogId equals post.BlogId into g
    //                        from post in g.DefaultIfEmpty()
    //                        join postTag in context.PostTags on post.PostId equals postTag.PostId into h
    //                        from postTag in h.DefaultIfEmpty()
    //                        join tag in context.Tags on postTag.TagId equals tag.TagId into m
    //                        from tag in m.DefaultIfEmpty()
    //                        select new
    //                        {
    //                            blog,
    //                            post,
    //                            postTag,
    //                            tag
    //                        };
    //            var blogs = query.ToList();
    //        }
    //    }
    //}
}