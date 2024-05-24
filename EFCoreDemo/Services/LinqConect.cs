using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Services
{
    public class LinqConect
    {

        [Benchmark]
        public static void SingleInclude(SchoolContext context)
        {
            var data = context.Enrollments.Include(x => x.Student).ToList();
            
        }

        [Benchmark]
        public static void SingleLINQ(SchoolContext context)
        {
            var query = from a in context.Enrollments
                        join b in context.Students on a.StudentID equals b.ID into g
                        from b in g.DefaultIfEmpty()
                        select new
                        {
                            a,
                            b
                        };

            var db = query.ToList();

        }

        //[Benchmark]
        //public void MultipleInclude()
        //{
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Posts)
        //        .Include(blog => blog.Owner)
        //        .ToList();
        //}

        //[Benchmark]
        //public void MultipleLINQ()
        //{
        //    var query = from a in context.Blogs
        //                join b in context.Posts on a.BlogId equals b.BlogId into g
        //                from b in g.DefaultIfEmpty()
        //                join c in context.Person on a.OwnerId equals c.PersonId into m
        //                from c in m.DefaultIfEmpty()
        //                select new
        //                {
        //                    a,
        //                    b,
        //                    c
        //                };
        //    var blogs = query.ToList();

        //}

        //[Benchmark]
        //public void SingleChildInclude()
        //{
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Posts)
        //        .ThenInclude(post => post.Author)
        //        .ToList();
        //}

        //[Benchmark]
        //public void SingleChildLINQ()
        //{
        //    var query0 =
        //        from post in context.Posts
        //        join person in context.Person on post.AuthorId equals person.PersonId into g
        //        from person in g.DefaultIfEmpty()
        //        select new
        //        {
        //            post,
        //            person
        //        };

        //    var query1 =
        //        from a in context.Posts
        //        join b in query0 on a.AuthorId equals b.post.BlogId into g
        //        from b in g.DefaultIfEmpty()
        //        select new
        //        {
        //            a,
        //            b.post,
        //            b.person
        //        };

        //    var blogs = query1.ToList();
        //}


        //[Benchmark]
        //public void MultipleThenIncludes()
        //{
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Posts)
        //        .ThenInclude(post => post.Author)
        //        .ThenInclude(author => author.Photo)
        //        .ToList();
        //}

        //[Benchmark]
        //public void IncludeTree()
        //{
        //    using var context = new BloggingContext();
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Posts)
        //        .ThenInclude(post => post.Author)
        //        .ThenInclude(author => author.Photo)
        //        .Include(blog => blog.Owner)
        //        .ThenInclude(owner => owner.Photo)
        //        .ToList();
        //}

        //[Benchmark]
        //public void MultipleLeafIncludes()
        //{
        //    using var context = new BloggingContext();
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Posts)
        //        .ThenInclude(post => post.Author)
        //        .Include(blog => blog.Posts)
        //        .ThenInclude(post => post.Tags)
        //        .ToList();
        //}

        //[Benchmark]
        //public void IncludeMultipleNavigationsWithSingleInclude()
        //{
        //    using var context = new BloggingContext();
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Owner.AuthoredPosts)
        //        .ThenInclude(post => post.Blog.Owner.Photo)
        //        .ToList();
        //}

        //[Benchmark]
        //public void MultipleLeafIncludesFiltered2()
        //{
        //    using var context = new BloggingContext();
        //    var filteredBlogs = context.Blogs
        //        .Include(blog => blog.Posts.Where(post => post.BlogId == 1))
        //        .ThenInclude(post => post.Author)
        //        .Include(blog => blog.Posts.Where(post => post.BlogId == 1))
        //        .ThenInclude(post => post.Tags.OrderBy(postTag => postTag.TagId).Skip(3))
        //        .ToList();
        //}

        //[Benchmark]
        //public void LeftOuterJoin()
        //{
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Posts)
        //        .ThenInclude(post => post.Tags)
        //        .ThenInclude(tags => tags.Tag)
        //        .ToList();
        //}

        //[Benchmark]
        //public void LeftOuterJoinOrderBy()
        //{
        //    var blogs = context.Blogs
        //        .Include(blog => blog.Posts)
        //        .ThenInclude(post => post.Tags)
        //        .ThenInclude(tags => tags.Tag)
        //        .OrderBy(a => a.BlogId)
        //        .ToList();
        //}

        //[Benchmark]
        //public void LeftOuterJoinWithLinQ()
        //{
        //    var query = from blog in context.Blogs
        //                join post in context.Posts on blog.BlogId equals post.BlogId into g
        //                from post in g.DefaultIfEmpty()
        //                join postTag in context.PostTags on post.PostId equals postTag.PostId into h
        //                from postTag in h.DefaultIfEmpty()
        //                join tag in context.Tags on postTag.TagId equals tag.TagId into m
        //                from tag in m.DefaultIfEmpty()
        //                select new
        //                {
        //                    blog,
        //                    post,
        //                    postTag,
        //                    tag
        //                };

        //    var blogs = query.ToList();

        //}
    }
}
