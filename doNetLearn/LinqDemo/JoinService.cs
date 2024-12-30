using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static doNetLearn.LinqDemo.Models.Class1;
using System.Xml.Linq;

namespace doNetLearn.LinqDemo
{
    internal class JoinService
    {
        public static void Demo()
        {
            //联接操作 Join   GroupJoin
            var s1 = new Student("Lily", "L", 1, GradeLevel.FirstYear, new List<int>() { 98, 99, 89 }, 1);
            var s2 = new Student("Lucy", "C", 2, GradeLevel.SecondYear, new List<int>() { 88, 87, 79 }, 2);
            var s3 = new Student("Linda", "L", 1, GradeLevel.FirstYear, new List<int>() { 90, 89, 85 }, 1);
            var s4 = new Student("Amy", "C", 2, GradeLevel.SecondYear, new List<int>() { 76, 67, 72 }, 2);
            var s5 = new Student("Amy", "A", 2, GradeLevel.SecondYear, new List<int>() { 80, 75, 83 }, 4);
            var students = new List<Student> { s1, s2, s3, s4, s5 };

            var t1 = new Teacher("Amy", "A", 921, "Londan");
            var t2 = new Teacher("Lily", "L", 965, "Paris");
            var teachers = new List<Teacher> { t1, t2 };

            var d1 = new Department("英文", 1, 1);
            var d2 = new Department("中文", 2, 2);
            var d3 = new Department("数学", 3, 1);
            var departments = new List<Department> { d1, d2, d3 };

            Console.WriteLine("下面的示例使用 join … in … on … equals … 子句基于特定值联接两个序列：");
            {
                var query = from student in students
                            join department in departments on student.DepartmentID equals department.ID
                            select new { Name = $"{student.FirstName} {student.LastName}", DepartmentName = department.Name };

                foreach (var item in query)
                {
                    Console.WriteLine($"{item.Name} - {item.DepartmentName}");
                }
            }

            {
                Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
                var query = students.Join(departments,
                student => student.DepartmentID, department => department.ID,
                (student, department) => new { Name = $"{student.FirstName} {student.LastName}", DepartmentName = department.Name });

                foreach (var item in query)
                {
                    Console.WriteLine($"{item.Name} - {item.DepartmentName}");
                }
            }

            //对结果进行了分组
            Console.WriteLine("使用 join … in … on … equals … into … 子句基于特定值联接两个序列，并对每个元素的结果匹配项进行分组：");
            {
                IEnumerable<IEnumerable<Student>> studentGroups = from department in departments
                                                                  join student in students on department.ID equals student.DepartmentID into studentGroup
                                                                  select studentGroup;

                foreach (IEnumerable<Student> studentGroup in studentGroups)
                {
                    Console.WriteLine("Group");
                    foreach (Student student in studentGroup)
                    {
                        Console.WriteLine($"  - {student.FirstName}, {student.LastName}");
                    }
                }

            }

            {
                Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
                // Join department and student based on DepartmentId and grouping result
                IEnumerable<IEnumerable<Student>> studentGroups = departments.GroupJoin(students,
                    department => department.ID, student => student.DepartmentID,
                    (department, studentGroup) => studentGroup);

                foreach (IEnumerable<Student> studentGroup in studentGroups)
                {
                    Console.WriteLine("Group");
                    foreach (Student student in studentGroup)
                    {
                        Console.WriteLine($"  - {student.FirstName}, {student.LastName}");
                    }
                }
            }

            Console.WriteLine("执行内部联接 以下示例演示如何执行内部联接的四种变体：");
            Console.WriteLine("单键联接");
            {

                var query = from department in departments
                            join teacher in teachers on department.TeacherID equals teacher.ID
                            select new
                            {
                                DepartmentName = department.Name,
                                TeacherName = $"{teacher.First} {teacher.Last}"
                            };

                foreach (var departmentAndTeacher in query)
                {
                    Console.WriteLine($"{departmentAndTeacher.DepartmentName} is managed by {departmentAndTeacher.TeacherName}");
                }

            }

            {
                Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
                var query = teachers
                .Join(departments, teacher => teacher.ID, department => department.TeacherID,
                    (teacher, department) =>
                    new { DepartmentName = department.Name, TeacherName = $"{teacher.First} {teacher.Last}" });

                foreach (var departmentAndTeacher in query)
                {
                    Console.WriteLine($"{departmentAndTeacher.DepartmentName} is managed by {departmentAndTeacher.TeacherName}");
                }
            }

            {
                Console.WriteLine("组合键联接-可以使用复合键基于多个属性来比较元素，而不是只基于一个属性使元素相关联:");
                // Join the two data sources based on a composite key consisting of first and last name,
                // to determine which employees are also students.
                IEnumerable<string> query =
                    from teacher in teachers
                    join student in students on new
                    {
                        FirstName = teacher.First,
                        LastName = teacher.Last
                    } equals new
                    {
                        student.FirstName,
                        student.LastName
                    }
                    select teacher.First + " " + teacher.Last;

                string result = "The following people are both teachers and students:\r\n";
                foreach (string name in query)
                {
                    result += $"{name}\r\n";
                }
                Console.Write(result);
            }


            {
                Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
                IEnumerable<string> query = teachers
                .Join(students,
                    teacher => new { FirstName = teacher.First, LastName = teacher.Last },
                    student => new { student.FirstName, student.LastName },
                    (teacher, student) => $"{teacher.First} {teacher.Last}"
             );

                Console.WriteLine("The following people are both teachers and students:");
                foreach (string name in query)
                {
                    Console.WriteLine(name);
                }
            }


            Console.WriteLine("多联接-可以将任意数量的联接操作相互追加，以执行多联接。 C# 中的每个 join 子句会将指定数据源与上一个联接的结果相关联。");
            {
                // The first join matches Department.ID and Student.DepartmentID from the list of students and
                // departments, based on a common ID. The second join matches teachers who lead departments
                // with the students studying in that department.
                var query = from student in students
                            join department in departments on student.DepartmentID equals department.ID
                            join teacher in teachers on department.TeacherID equals teacher.ID
                            select new
                            {
                                StudentName = $"{student.FirstName} {student.LastName}",
                                DepartmentName = department.Name,
                                TeacherName = $"{teacher.First} {teacher.Last}"
                            };
                foreach (var obj in query)
                {
                    Console.WriteLine($"""The student "{obj.StudentName}" studies in the department run by "{obj.TeacherName}".""");
                }
            }

            {
                Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
                var query = students
                .Join(departments, student => student.DepartmentID, department => department.ID,
                    (student, department) => new { student, department })
                .Join(teachers, commonDepartment => commonDepartment.department.TeacherID, teacher => teacher.ID,
                    (commonDepartment, teacher) => new
                    {
                        StudentName = $"{commonDepartment.student.FirstName} {commonDepartment.student.LastName}",
                        DepartmentName = commonDepartment.department.Name,
                        TeacherName = $"{teacher.First} {teacher.Last}"
                    });
                foreach (var obj in query)
                {
                    Console.WriteLine($"""The student "{obj.StudentName}" studies in the department run by "{obj.TeacherName}".""");
                }
            }


            Console.WriteLine("使用分组联接的内部联接");
            {
                var query1 =
                    from department in departments
                    join student in students on department.ID equals student.DepartmentID into gj
                    from subStudent in gj
                    select new
                    {
                        DepartmentName = department.Name,
                        StudentName = $"{subStudent.FirstName} {subStudent.LastName}"
                    };
                Console.WriteLine("Inner join using GroupJoin():");
                foreach (var v in query1)
                {
                    Console.WriteLine($"{v.DepartmentName} - {v.StudentName}");
                }
            }

            {
                Console.WriteLine("可以使用 GroupJoin 方法实现相同的结果，如下所示：");
                var queryMethod1 = departments
                .GroupJoin(students, department => department.ID, student => student.DepartmentID,
                    (department, gj) => new { department, gj })
                .SelectMany(departmentAndStudent => departmentAndStudent.gj,
                    (departmentAndStudent, subStudent) => new
                    {
                        DepartmentName = departmentAndStudent.department.Name,
                        StudentName = $"{subStudent.FirstName} {subStudent.LastName}"
                    });
                Console.WriteLine("Inner join using GroupJoin():");
                foreach (var v in queryMethod1)
                {
                    Console.WriteLine($"{v.DepartmentName} - {v.StudentName}");
                }
            }


            {
                Console.WriteLine("该结果等效于通过使用 join 子句（不使用 into 子句）执行内部联接来获取的结果集。 以下代码演示了此等效查询：");
                var query2 = from department in departments
                             join student in students on department.ID equals student.DepartmentID
                             select new
                             {
                                 DepartmentName = department.Name,
                                 StudentName = $"{student.FirstName} {student.LastName}"
                             };

                Console.WriteLine("The equivalent operation using Join():");
                foreach (var v in query2)
                {
                    Console.WriteLine($"{v.DepartmentName} - {v.StudentName}");
                }
            }


            {
                Console.WriteLine("为避免链接，可以使用单个 Join 方法，如此处所示：");
                var queryMethod2 = departments.Join(students, departments => departments.ID, student => student.DepartmentID,
                (department, student) => new
                {
                    DepartmentName = department.Name,
                    StudentName = $"{student.FirstName} {student.LastName}"
                });

                Console.WriteLine("The equivalent operation using Join():");
                foreach (var v in queryMethod2)
                {
                    Console.WriteLine($"{v.DepartmentName} - {v.StudentName}");
                }
            }

            Console.WriteLine("分组联接对于生成分层数据结构十分有用。 它将第一个集合中的每个元素与第二个集合中的一组相关元素进行配对。");
            {
                Console.WriteLine("分组联接");
                var query = from department in departments
                            join student in students on department.ID equals student.DepartmentID into studentGroup
                            select new
                            {
                                DepartmentName = department.Name,
                                Students = studentGroup
                            };

                foreach (var v in query)
                {
                    // Output the department's name.
                    Console.WriteLine($"{v.DepartmentName}:");

                    // Output each of the students in that department.
                    foreach (Student? student in v.Students)
                    {
                        Console.WriteLine($"  {student.FirstName} {student.LastName}");
                    }
                }
            }

            {
                Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
                var query = departments.GroupJoin(students, department => department.ID, student => student.DepartmentID,
                (department, Students) => new { DepartmentName = department.Name, Students });

                foreach (var v in query)
                {
                    // Output the department's name.
                    Console.WriteLine($"{v.DepartmentName}:");

                    // Output each of the students in that department.
                    foreach (Student? student in v.Students)
                    {
                        Console.WriteLine($"  {student.FirstName} {student.LastName}");
                    }
                }
            }


            //用于创建 XML 的分组联接
            Console.WriteLine("分组联接非常适合于使用 LINQ to XML 创建 XML。 下面的示例类似于上面的示例，不过结果选择器函数不会创建匿名类型，而是创建表示联接对象的 XML 元素。");
            {
                XElement departmentsAndStudents = new("DepartmentEnrollment",
                from department in departments
                join student in students on department.ID equals student.DepartmentID into studentGroup
                select new XElement("Department",
                    new XAttribute("Name", department.Name),
                    from student in studentGroup
                    select new XElement("Student",
                        new XAttribute("FirstName", student.FirstName),
                        new XAttribute("LastName", student.LastName)
                    )
                )
            );

                Console.WriteLine(departmentsAndStudents);
            }


            Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
            {
                XElement departmentsAndStudents = new("DepartmentEnrollment",
                departments.GroupJoin(students, department => department.ID, student => student.DepartmentID,
                    (department, Students) => new XElement("Department",
                        new XAttribute("Name", department.Name),
                        from student in Students
                        select new XElement("Student",
                            new XAttribute("FirstName", student.FirstName),
                            new XAttribute("LastName", student.LastName)
                        )
                    )
                )
            );
                Console.WriteLine(departmentsAndStudents);
            }


            //执行左外部联接
            Console.WriteLine("左外部联接是这样定义的：返回第一个集合的每个元素，无论该元素在第二个集合中是否有任何相关元素。 可以使用 LINQ 通过对分组联接的结果调用 DefaultIfEmpty 方法来执行左外部联接。");
            {
                var query =
                from student in students
                join department in departments on student.DepartmentID equals department.ID into gj
                from subgroup in gj.DefaultIfEmpty()
                select new
                {
                    student.FirstName,
                    student.LastName,
                    Department = subgroup?.Name ?? string.Empty
                };

                foreach (var v in query)
                {
                    Console.WriteLine($"{v.FirstName:-15} {v.LastName:-15}: {v.Department}");
                }
            }


            {
                Console.WriteLine("可以使用方法语法来表示前面的查询，如以下代码所示：");
                var query = students.GroupJoin(departments, student => student.DepartmentID, department => department.ID,
                (student, department) => new { student, subgroup = department }) //DefaultIfEmpty
                .Select(gj => new
                {
                    gj.student.FirstName,
                    gj.student.LastName,
                    Department = gj.subgroup?.FirstOrDefault()?.Name ?? string.Empty
                });

                foreach (var v in query)
                {
                    Console.WriteLine($"{v.FirstName:-15} {v.LastName:-15}: {v.Department}");
                }
            }

        }
    }
}
