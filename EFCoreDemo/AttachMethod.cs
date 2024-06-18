using EFCoreDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    internal class AttachMethod
    {
        private readonly SchoolContext  _schoolContext ;

        public AttachMethod(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void Method1()
        {
            var course = _schoolContext.Courses.FirstOrDefault();
            string json= System.Text.Json.JsonSerializer.Serialize(course);
            
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
