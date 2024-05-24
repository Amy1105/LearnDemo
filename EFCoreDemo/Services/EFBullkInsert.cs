using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using EFCore.BulkExtensions;
using EFCoreDemo.Models;

namespace EFCoreDemo.Services
{
    public class EFBullkInsert
    {
        private const int Count = 10000;

        #region 单体批量新增
        [Benchmark]
        public static void AddStudents(SchoolContext context)
        {
            //一个学生可以有多门课，先添加课

            List<Student> students = new List<Student>();
            foreach (var i in Enumerable.Range(1, Count))
            {
                students.Add(new Student
                {
                    FirstMidName = "Gytis" + i.ToString(),
                    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2018-09-01"),
                });
            }
            context.Students.AddRange(students);
            context.SaveChanges();
        }

        [Benchmark]
        public static void AddStudentsWithBullk(SchoolContext context)
        {
            List<Student> students = new List<Student>();
            foreach (var i in Enumerable.Range(1, Count))
            {
                students.Add(new Student
                {
                    FirstMidName = "GytisBullk" + i.ToString(),
                    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2019-09-01"),
                });
            }
            context.BulkInsert(students);
        }
        #endregion

        #region  联表新增
        [Benchmark]
        public static Task AddConectTablesAsync(SchoolContext context)
        {           
            context.Courses.AddRange(GetCourses());
            return context.SaveChangesAsync();
        }

        [Benchmark]
        public static Task AddConectTablesWithBullkAsync(SchoolContext context)
        {
            context.BulkInsertAsync(GetCourses());
            return context.BulkSaveChangesAsync();
        }

        #endregion

        /// <summary>
        /// 构建课程实体（联表）
        /// </summary>
        /// <returns></returns>
        public static List<Course> GetCourses()
        {
            List<Course> Courses = new List<Course>();
            foreach (var i in Enumerable.Range(1, Count))
            {
                //这门课下的3个老师
                List<Instructor> instructors = new List<Instructor>();
                foreach (var j in Enumerable.Range(1, 3))
                {
                    instructors.Add(new Instructor
                    {
                        FirstMidName = "Kim" + i.ToString() + "-" + j.ToString(),
                        LastName = "Abercrombie" + i.ToString() + "-" + j.ToString(),
                        HireDate = DateTime.Parse("1995-03-11"),
                    });
                }
                //这门课所属部门
                var engineering = new Department
                {
                    Name = "Engineering" + i.ToString(),
                    Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                };
                //这门课
                Courses.Add(new Course
                {
                    Title = "Literature",
                    Credits = 4,
                    Instructors = instructors,
                    Department = engineering
                });
            }
            return Courses;
        }


        #region  新增或修改



        #endregion

    }
}
