using BenchmarkDotNet.Attributes;
using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EFCoreDemo.Services
{
    public class LinqConect
    {
        private readonly SchoolContext context;
        public LinqConect(SchoolContext _context)
        {
            context= _context;
        }
     

        /*
         * SELECT "o"."Id", "o"."AddressID", "o"."CreateTime", "o"."IsPay", "o"."PayTime", "o0"."Id", "o0"."Amount", "o0"."Count", "o0"."Description", "o0"."OrderId", "o0"."Price", "o0"."ProductName"
      FROM "Orders" AS "o"
      LEFT JOIN "OrderDetails" AS "o0" ON "o"."Id" = "o0"."OrderId"
      ORDER BY "o"."Id"
         */
        public void SingleInclude()
        {
            var data = context.Orders.Include(x => x.OrderDetails).ToList();
            foreach (var d in data)
            {
                Console.WriteLine($"order:{d.Id},{d.CreateTime.ToString("yyyy-hh-dd hh:mm:ss")}，地址：{d.address.ToString()}");

                foreach (var od in d.OrderDetails)
                {
                    Console.WriteLine($"----:{od.ProductName}-{od.Count}/{od.Price}={od.Amount}");
                }
            }
        }

        /*
          SELECT "o"."Id", "o"."AddressID", "o"."CreateTime", "o"."IsPay", "o"."PayTime", "o0"."Id", "o0"."Amount", "o0"."Count", "o0"."Description", "o0"."OrderId", "o0"."Price", "o0"."ProductName"
      FROM "Orders" AS "o"
      LEFT JOIN "OrderDetails" AS "o0" ON "o"."Id" = "o0"."OrderId"
         */

        public void SingleLINQ()
        {
           
            var query = from a in context.Orders  
                        join b in context.OrderDetails on a.Id equals b.OrderId into g
                        from b in g.DefaultIfEmpty()
                        select new
                        {
                          a,b
                        };

            var data = query.ToList();
            foreach (var d in data)
            {
                Console.WriteLine($"order:{d.a.Id},{d.a.CreateTime.ToString("yyyy-hh-dd hh:mm:ss")}，地址：{d.a.address.ToString()}");

                foreach (var od in d.a.OrderDetails)
                {
                    Console.WriteLine($"----:{od.ProductName}-{od.Count}/{od.Price}={od.Amount}");
                }
            }
        }

        /*
           SELECT "o"."Id", "o"."AddressID", "o"."CreateTime", "o"."IsPay", "o"."PayTime", "a"."Id", "o0"."Id", "o0"."Amount", "o0"."Count",
        "o0"."Description", "o0"."OrderId", "o0"."Price", "o0"."ProductName", "a"."City", "a"."District", "a"."Name", 
        "a"."Phone", "a"."Postal_code", "a"."Province", "a"."Street"
      FROM "Orders" AS "o"
      INNER JOIN "Addresss" AS "a" ON "o"."AddressID" = "a"."Id"
      LEFT JOIN "OrderDetails" AS "o0" ON "o"."Id" = "o0"."OrderId"
      ORDER BY "o"."Id", "a"."Id"
         */
        [Benchmark]
        public void MultipleInclude()
        {
            var blogs = context.Orders
                .Include(blog => blog.OrderDetails)
                .Include(blog => blog.address)
                .ToList();
        }


        /***
          SELECT "o"."Id", "o"."AddressID", "o"."CreateTime", "o"."IsPay", "o"."PayTime", "o0"."Id", "o0"."Amount", "o0"."Count", 
        "o0"."Description", "o0"."OrderId", "o0"."Price", "o0"."ProductName", "a"."Id", "a"."City", "a"."District", "a"."Name", 
        "a"."Phone", "a"."Postal_code", "a"."Province", "a"."Street"
      FROM "Orders" AS "o"
      LEFT JOIN "OrderDetails" AS "o0" ON "o"."Id" = "o0"."OrderId"
      LEFT JOIN "Addresss" AS "a" ON "o"."AddressID" = "a"."Id"
         */
        [Benchmark]
        public void MultipleLINQ()
        {
            var query = from a in context.Orders
                        join b in context.OrderDetails on a.Id equals b.OrderId into g
                        from b in g.DefaultIfEmpty()
                        join c in context.Addresss on a.AddressID equals c.Id into m
                        from c in m.DefaultIfEmpty()
                        select new
                        {
                            a,
                            b,
                            c
                        };
            var blogs = query.ToList();

        }

        /*
          SELECT "o"."Id", "o"."AddressID", "o"."CreateTime", "o"."IsPay", "o"."PayTime", "o0"."Id", "o0"."Amount", "o0"."Count", "o0"."Description", "o0"."OrderId", "o0"."Price", "o0"."ProductName", "a"."Id", "a"."City", "a"."District", "a"."Name", "a"."Phone", "a"."Postal_code", "a"."Province", "a"."Street"
      FROM "Orders" AS "o"
      LEFT JOIN "OrderDetails" AS "o0" ON "o"."Id" = "o0"."OrderId"
      INNER JOIN "Addresss" AS "a" ON "o"."AddressID" = "a"."Id"
         * 
         */

        [Benchmark]
        public void MultipleLINQ2()
        {
            var query = from a in context.Orders
                        join b in context.OrderDetails on a.Id equals b.OrderId into g
                        from b in g.DefaultIfEmpty()
                        join c in context.Addresss on a.AddressID equals c.Id into m
                        from c in m
                        select new
                        {
                            a,
                            b,
                            c
                        };
            var blogs = query.ToList();

        }

        /*
          SELECT "o"."Id", "o"."Amount", "o"."Count", "o"."Description", "o"."OrderId", "o"."Price", "o"."ProductName", 
        "o0"."Id", "o0"."AddressID", "o0"."CreateTime", "o0"."IsPay", "o0"."PayTime", "a"."Id", "a"."City", "a"."District", 
        "a"."Name", "a"."Phone", "a"."Postal_code", "a"."Province", "a"."Street"
      FROM "OrderDetails" AS "o"
      INNER JOIN "Orders" AS "o0" ON "o"."OrderId" = "o0"."Id"
      INNER JOIN "Addresss" AS "a" ON "o0"."AddressID" = "a"."Id"
         * 
         */

        [Benchmark]
        public void SingleChildInclude()
        {
            var blogs = context.OrderDetails
                .Include(blog => blog.order)
                .ThenInclude(post => post.address)
                .ToList();
        }

        /*
          SELECT "a"."Id", "a"."City", "a"."District", "a"."Name", "a"."Phone", "a"."Postal_code", "a"."Province", "a"."Street", 
        "t"."Id", "t"."Amount", "t"."Count", "t"."Description", "t"."OrderId", "t"."Price", "t"."ProductName", "t"."Id0",
        "t"."AddressID", "t"."CreateTime", "t"."IsPay", "t"."PayTime"
      FROM "Addresss" AS "a"
      LEFT JOIN (
          SELECT "o"."Id", "o"."Amount", "o"."Count", "o"."Description", "o"."OrderId", "o"."Price", "o"."ProductName", 
        "o0"."Id" AS "Id0", "o0"."AddressID", "o0"."CreateTime", "o0"."IsPay", "o0"."PayTime"
          FROM "OrderDetails" AS "o"
          LEFT JOIN "Orders" AS "o0" ON "o"."OrderId" = "o0"."Id"
      ) AS "t" ON "a"."Id" = "t"."AddressID"
         * 
         */


        [Benchmark]
        public void SingleChildLINQ()
        {
            var query0 =
                from post in context.OrderDetails
                join person in context.Orders on post.OrderId equals person.Id into g
                from person in g.DefaultIfEmpty()
                select new
                {
                    post,
                    person
                };

            var query1 =
                from a in context.Addresss
                join b in query0 on a.Id equals b.person.AddressID into g
                from b in g.DefaultIfEmpty()
                select new
                {
                    a,
                    b.post,
                    b.person
                };
            var blogs = query1.ToList();
        }


        [Benchmark]
        public void MultipleThenIncludes()
        {
            var blogs = context.Orders
                .Include(blog => blog.OrderDetails)
                .ThenInclude(post => post.product)
                .ThenInclude(author => author.Images)
                .ToList();
        }

        /*
          SELECT "o"."Id", "o"."AddressID", "o"."CreateTime", "o"."IsPay", "o"."PayTime", "a"."Id", "t"."Id", 
        "t"."Amount", "t"."Count", "t"."Description", "t"."OrderId", "t"."Price", "t"."ProductID", 
        "t"."ProductName", "t"."Id0", "t"."Count0", "t"."Description0", "t"."ImagesId", "t"."Price0", 
        "t"."ProductName0", "t"."Id1", "t"."Images", "t"."MainImage", "a"."City", "a"."District", "a"."Name",
        "a"."Phone", "a"."Postal_code", "a"."Province", "a"."Street"
      FROM "Orders" AS "o"
      INNER JOIN "Addresss" AS "a" ON "o"."AddressID" = "a"."Id"
      LEFT JOIN (
          SELECT "o0"."Id", "o0"."Amount", "o0"."Count", "o0"."Description", "o0"."OrderId", "o0"."Price", "o0"."ProductID", 
        "o0"."ProductName", "p"."Id" AS "Id0", "p"."Count" AS "Count0", "p"."Description" AS "Description0", "p"."ImagesId",
        "p"."Price" AS "Price0", "p"."ProductName" AS "ProductName0", "i"."Id" AS "Id1", "i"."Images", "i"."MainImage"
          FROM "OrderDetails" AS "o0"
          INNER JOIN "Products" AS "p" ON "o0"."ProductID" = "p"."Id"
          INNER JOIN "Images" AS "i" ON "p"."ImagesId" = "i"."Id"
      ) AS "t" ON "o"."Id" = "t"."OrderId"
      ORDER BY "o"."Id", "a"."Id", "t"."Id", "t"."Id0"
         * 
         * 
         */
        [Benchmark]
        public void IncludeTree()
        {           
            var blogs = context.Orders
                .Include(blog => blog.OrderDetails)
                .ThenInclude(post => post.product)
                .ThenInclude(author => author.Images)
                .Include(blog => blog.address)               
                .ToList();
        }

        [Benchmark]
        public void IncludeTree2()
        {
            var blogs = context.OrderDetails
                .Include(blog => blog.product)
                .Include(post => post.order)                
                .ThenInclude(post=>post.address)
                .Include(blog => blog.product.Images)

                .ToList();
        }

        [Benchmark]
        public void MultipleLeafIncludes()
        {           
            var blogs = context.Orders
                .Include(blog => blog.OrderDetails)
                .ThenInclude(post => post.product)
                .Include(blog => blog.address)               
                .ToList();
        }

        [Benchmark]
        public void IncludeMultipleNavigationsWithSingleInclude()
        {         
            var blogs = context.Orders
                .Include(blog => blog.address)               
                .ToList();
        }

        //[Benchmark]
        //public void MultipleLeafIncludesFiltered2()
        //{           
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
