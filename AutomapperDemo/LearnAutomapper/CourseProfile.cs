using AutoMapper;
using LearnAutomapper.Dto;
using LearnAutomapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutomapper
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<CourseInstructorDto, CourseInstructor>().ReverseMap();
            CreateMap<CourseInstructorDepartmentDto, CourseInstructorDepartment>().ReverseMap();

            CreateMap<ex_OrderDetailDto, ex_OrderDetail>().ReverseMap();
        }
    }
}
