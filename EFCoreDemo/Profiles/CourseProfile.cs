using AutoMapper;
using AutoMapper.EquivalencyExpression;
using EFCoreDemo.AutoMapperModels;
using EFCoreDemo.Dto;
using EFCoreDemo.Models;
using Microsoft.Diagnostics.Tracing.StackSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Profiles
{
    internal class CourseProfile :Profile
    {
        public CourseProfile()
        {


            CreateMap<Course, CourseDto>().ReverseMap();//.EqualityComparison((odto, o) => odto.CourseID == o.CourseID);


     
            // CreateMap<CourseDto,Course>();

            CreateMap<Instructor, InstructorDto>().ReverseMap();

            //

            var cfg=CreateMap<Source, Destination>().ReverseMap();

            //AllowNullCollections 如何配置  to  do。。。

            //var configuration = new MapperConfiguration(cfg => {
            //    cfg.AllowNullCollections = true;
            //    cfg.CreateMap<Source, Destination>();
            //});

            //var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Source, Destination>());


            //多态复杂对象的映射配置
            CreateMap<ParentSource, ParentDestination>().Include<ChildSource, ChildDestination>().ReverseMap();
            CreateMap<ChildSource, ChildDestination>().ReverseMap();
         
            //使用构造函数，映射

            CreateMap<SourceBuild, SourceBuildDto>().ForCtorParam("valueParamSomeOtherName", opt => opt.MapFrom(src => src.Value));

            //禁用构造函数
            //var configuration = new MapperConfiguration(cfg => cfg.DisableConstructorMapping());

            //指定使用public的构造函数
            //var configuration = new MapperConfiguration(cfg => cfg.ShouldUseConstructor = constructor => constructor.IsPublic);


            CreateMap<Order, OrderDto>().ReverseMap();



        }
    }
}
