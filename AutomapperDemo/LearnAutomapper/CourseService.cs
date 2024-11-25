﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearnAutomapper.Dto;
using LearnAutomapper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace LearnAutomapper
{
    public class CourseService
    {

        private readonly SchoolContext context;
       // private readonly Logger<CourseService> logger;
        private readonly IMapper mapper;

        public CourseService(SchoolContext context,IMapper mapper)
        {
            this.context = context;
           // this.logger = logger;
            this.mapper = mapper;
        }

        public void InitDB()
        {
            context.Database.EnsureDeleted();
            //建库
            context.Database.EnsureCreated();   
            
            Console.WriteLine("data init.");
        }

        public async Task AddCourse()
        {
            List<DepartmentDto> departments = new List<DepartmentDto>()
                {
                    new DepartmentDto (){ Name="departmentA", Budget=1.1M},
                    new DepartmentDto (){Name="departmentB", Budget=2.2M},
                };
            List<InstructorDto> instructorDtos = new List<InstructorDto>()
            {
              new InstructorDto(){LastName="Kapoor",FirstMidName="Candace",Departments=departments},
              new InstructorDto(){LastName="Amy",FirstMidName="Lucy",Departments=departments}
            };
            CourseDto courseDto = new CourseDto() { Title = "Chemistry", Credits = 1,Instructors= instructorDtos };  //instructorDtos
            var course = mapper.Map<Course>(courseDto);
            context.Courses.Add(course);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <returns></returns>
        public async Task AddCourses()
        {
            List<CourseDto> courses = new List<CourseDto>();
            foreach (var i in Enumerable.Range(1, 50))
            {
                List<DepartmentDto> departments = new List<DepartmentDto>() 
                {
                    new DepartmentDto (){ Name="departmentA"+i, Budget=1.1M},
                    new DepartmentDto (){Name="departmentB"+i, Budget=2.2M},
                };

                List<InstructorDto> instructorDtos = new List<InstructorDto>()
                {
                      new InstructorDto(){LastName="Kapoor"+i,FirstMidName="Candace"+i,Departments=departments},
                      new InstructorDto(){LastName="Amy"+i,FirstMidName="Lucy"+i,Departments=departments}
                };
                courses.Add(new CourseDto() { Title = "Chemistry"+i, Credits = i, Instructors = instructorDtos });
            }              
            var course = mapper.Map<List<Course>>(courses);

            context.Courses.AddRange(course);
            await context.SaveChangesAsync();
        }

        public void SelectCourse()
        {
           var courses= context.Courses.Include(x => x.Instructors).ThenInclude(x => x.Departments).ToList();
            if(courses.Any())
            {
                foreach(var course in courses)
                {
                    Print(course);
                }               
            }
        }

        public void UpdateCourse(int courseID)
        {
            var dbCourse = context.Courses.Where(x => x.CourseID == courseID).Include(x => x.Instructors).ThenInclude(x => x.Departments);
            var courseDto = mapper.ProjectTo<CourseDto>(dbCourse);
            Print(courseDto.First());
        }

        private void Print(Course course)
        {
            if(course != null)
            {
                Console.WriteLine($"课程:{course.CourseID},{course.Title}");
                if(course.Instructors.Any())
                {
                    foreach (var instructor in course.Instructors)
                    {
                        Console.WriteLine($"----教师 :{instructor.ID}-{instructor.FullName}-{instructor.CourseID}");
                        if(instructor.Departments.Any())
                        {
                            foreach (var department in instructor.Departments)
                            {
                                Console.WriteLine($"----部门 :{department.ID}-{department.Name}-{department.InstructorID}");
                            }
                        }
                    }
                }               
            }                      
        }

        

        private void Print(CourseDto course)
        {
            if (course != null)
            {
                Console.WriteLine($"课程:{course.CourseID},{course.Title}");
                if (course.Instructors.Any())
                {
                    foreach (var instructor in course.Instructors)
                    {
                        Console.WriteLine($"----教师 :{instructor.ID}-{instructor.FirstMidName}-{instructor.LastName}");
                        if (instructor.Departments.Any())
                        {
                            foreach (var department in instructor.Departments)
                            {
                                Console.WriteLine($"----部门 :{department.DepartmentID}-{department.Name}");
                            }
                        }
                    }
                }
            }
        }
    }
}
