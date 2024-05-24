using EFCore.BulkExtensions;
using EFCoreDemo.Models;

namespace EFCoreDemo.Services
{
    public class EFBullkQuery
    {
        public static void QueryStudents(SchoolContext context)
        {
            //一个学生可以有多门课，先添加课

            List<Student> students = new List<Student>(); 
            foreach (var i in Enumerable.Range(1, 100))
            {
                students.Add(new Student
                {
                    FirstMidName = "Gytis"+i.ToString(),
                    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2018-09-01"),                   
                });
            }
            context.Students.AddRange(students);
            context.SaveChanges();  
        }


        public static void QueryStudentsWithBullk(SchoolContext context)
        {
            List<Student> students = new List<Student>();
            foreach (var i in Enumerable.Range(101, 100))
            {
                students.Add(new Student
                {
                    FirstMidName = "GytisBullk"+i.ToString(),
                    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2019-09-01"),
                });
            }           
            context.BulkInsert(students);
        }
    }
}
