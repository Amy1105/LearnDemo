using AutoMapper;
using EFCoreDemo.AutoMapperModels;
using EFCoreDemo.Dto;
using EFCoreDemo.Models;
using Microsoft.Diagnostics.Tracing.StackSources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Services
{
    /// <summary>
    /// automapper 特性展示与分析
    /// </summary>
    internal class autoMapperDemo
    {
        private readonly IMapper _mapper;

        private readonly SchoolContext _schoolContext;

        public autoMapperDemo(IMapper mapper, SchoolContext schoolContext)
        {
            _mapper = mapper;
            _schoolContext = schoolContext;
        }

        /// <summary>
        /// 投影
        /// </summary>
        public void Projection()
        {

        }

        /// <summary>
        /// 嵌套投影:当映射到现有集合时，首先会清除目标集合。如果这不是您想要的，请查看AutoMapper.Collection。
        /// </summary>
        public void NestedProjection()
        {
            var sources = new[]
              {
                new Source { Value = 5 },
                new Source { Value = 6 },
                new Source { Value = 7 }
            };

            IEnumerable<Destination> ienumerableDest = _mapper.Map<Source[], IEnumerable<Destination>>(sources);
            ICollection<Destination> icollectionDest = _mapper.Map<Source[], ICollection<Destination>>(sources);
            IList<Destination> ilistDest = _mapper.Map<Source[], IList<Destination>>(sources);
            List<Destination> listDest = _mapper.Map<Source[], List<Destination>>(sources);
            Destination[] arrayDest = _mapper.Map<Source[], Destination[]>(sources);
        }

        public void PolymorphicComplexObjects()
        {
            var sources = new[]
               {
        new ParentSource(),
        new ChildSource(),
        new ParentSource()
    };

            var destinations = _mapper.Map<ParentSource[], ParentDestination[]>(sources);

            //ShouldBeInstanceOf  to  do ...
            //destinations[0].ShouldBeInstanceOf<ParentDestination>();
            //destinations[1].ShouldBeInstanceOf<ChildDestination>();
            //destinations[2].ShouldBeInstanceOf<ParentDestination>();
        }

        /// <summary>
        /// 建造，使用构造函数映射
        /// </summary>
        public void  BuildMethod()
        {

        }

        /// <summary>
        /// AutoMapper 会将目标成员名称拆分为单个单词（按照 PascalCase 约定）。
        /// </summary>
        public void Flattening()
        {
            // Complex model

            var customer = new Mapper_Customer
            {
                Name = "George Costanza"
            };
            var order = new Mapper_Order
            {
                Customer = customer
            };
            var bosco = new Mapper_Product
            {
                Name = "Bosco",
                Price = 4.99m
            };
            order.AddOrderLineItem(bosco, 15);
        
            OrderDto dto = _mapper.Map<Mapper_Order, OrderDto>(order);  //  异常  缺少类型映射配置或不支持的映射   to  do  ...
            Console.WriteLine($" dto.CustomerName:{dto.CustomerName} ShouldEqual George Costanza");
            Console.WriteLine($" dto.Total:{dto.Total} ShouldEqual 74.85m");
        }

        /// <summary>
        /// 多个 dto   映射 少的db
        /// </summary>
        public async Task Method1()
        {
            ////异常 json转复杂对象    to  do ...
            //string dtoStr = """
            //    {"CourseID":6,"Title":"Chemistry","Credits":3,"InstructorDtos":{[{"ID":4,"LastName":"Kapoor","FirstMidName":"Candace"},{"ID":5,"LastName":"Kapoor5","FirstMidName":"Candace5"}]}}
            //    """;
            //CourseDto? courseDto= System.Text.Json.JsonSerializer.Deserialize<CourseDto>(dtoStr);
            //if(courseDto == null )
            //{
            //    return;
            //}

            List<InstructorDto> instructorDtos= new List<InstructorDto>() 
            { 
              new InstructorDto(){ID=4,LastName="Kapoor444",FirstMidName="Candace444"},
              new InstructorDto(){ID=null,LastName="Kapoor555",FirstMidName="Candace555"}};

            CourseDto courseDto = new CourseDto() {CourseID=6,Title="Chemistry666",Credits=4 };  //instructorDtos
            //AddAutoMapper           
            if (courseDto.CourseID>0)
            {
                var dbCourse= _schoolContext.Courses.Where(x=>x.CourseID==courseDto.CourseID).Include(x=>x.Instructors).FirstOrDefault();
                if (dbCourse==null)
                {
                    return;
                }
                Console.WriteLine("---db修改前---");
                Common.Print(new List<Course>() { dbCourse });

                if(dbCourse!=null)
                {
                    var course = _mapper.Map<CourseDto, Course>(courseDto, dbCourse);
                    List<Instructor> instructors = _mapper.Map<List<InstructorDto>, List<Instructor>>(courseDto.InstructorDtos);
                    course.Instructors= instructors;
                    Console.WriteLine("---mapper转换后---");
                    Common.Print(new List<Course>() { course });

                    Console.WriteLine("---mapper转换后-dbCourse的情况---");
                    Common.Print(new List<Course>() { dbCourse });

                }                               
            }
            else
            {
                var course = _mapper.Map<CourseDto>(courseDto);
            }

           await  _schoolContext.SaveChangesAsync();
        }



        /// <summary>
        /// 少的 dto   映射 多的db  ，会修改掉db的未映射的数据吗
        /// </summary>
        public async Task Method2()
        {           
            CourseDto courseDto = new CourseDto() { CourseID = 6, Title = "Chemistry666", Credits = 4 };  //instructorDtos
            //AddAutoMapper           
            if (courseDto.CourseID > 0)
            {
                var dbCourse = _schoolContext.Courses.Where(x => x.CourseID >= courseDto.CourseID).Take(5).ToList();
                if (dbCourse == null)
                {
                    return;
                }
                Console.WriteLine("---db修改前---");
                Common.Print(dbCourse);

                if (dbCourse != null)
                {
                    var course = _mapper.Map<List<CourseDto>, List<Course>> (new List<CourseDto>() { courseDto }, dbCourse);                  
                    Console.WriteLine("---mapper转换后---");
                    Common.Print(course);
                    Console.WriteLine("---mapper转换后-dbCourse的情况---");
                    Common.Print(dbCourse);
                }
            }
            else
            {
                var course = _mapper.Map<CourseDto>(courseDto);
            }
            await _schoolContext.SaveChangesAsync();
        }

        /*
         * 
         如果 ID 匹配，则 AutoMapper 将把 OrderDTO 映射到 Order

如果 OrderDTO 存在而 Order 不存在，则 AutoMapper 将从 OrderDTO 映射的新 Order 添加到集合中

如果 Order 存在而 OrderDTO 不存在，则 AutoMapper 将从集合中删除 Order
         * 
         */

        //db 3

        //dto  2 new




    }
}
