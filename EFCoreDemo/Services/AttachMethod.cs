using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EFCoreDemo.Services
{
    internal class AttachMethod
    {
        private readonly SchoolContext _schoolContext;

        public AttachMethod(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void AddData()
        {
            Common.GetCourses(2000);
            _schoolContext.SaveChanges();
        }

        public void Method1()
        {
            //var courseList = _schoolContext.Courses.Include(x=>x.Instructors).Take(100).ToList();
            //Common.Print(courseList);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var course = _schoolContext.Courses.Include(x => x.Instructors).First(x => x.CourseID == 6);
            string json = JsonSerializer.Serialize(course, options);                      
        }

        public void Method2()
        {
            string objstr = """
                {"CourseID":1,"Title":"2312312","Credits":null,"IsDeleted":false,"Enrollments":null,"Instructors":null}
                """;
            var obj = System.Text.Json.JsonSerializer.Deserialize<Course>(objstr);
            _schoolContext.Courses.Attach(obj);
            _schoolContext.SaveChanges();
        }
    }
}
