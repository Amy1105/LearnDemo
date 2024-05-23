using LinqDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    internal class GroupByDemo
    {
        public static void Demo()
        {
            //分组 GroupBy  ToLookUp
            Console.WriteLine("下列代码示例根据奇偶性，使用 group by 子句对列表中的整数进行分组");
            {
                List<int> numbers = [35, 44, 200, 84, 3987, 4, 199, 329, 446, 208];

                IEnumerable<IGrouping<int, int>> query = from number in numbers
                                                         group number by number % 2;

                foreach (var group in query)
                {
                    Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
                    foreach (int i in group)
                    {
                        Console.WriteLine(i);
                    }
                }
            }

            {
                Console.WriteLine("以下代码显示了使用方法语法的等效查询");
                List<int> numbers = [35, 44, 200, 84, 3987, 4, 199, 329, 446, 208];

                IEnumerable<IGrouping<int, int>> query = numbers
                    .GroupBy(number => number % 2);

                foreach (var group in query)
                {
                    Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
                    foreach (int i in group)
                    {
                        Console.WriteLine(i);
                    }
                }
            }

            var s1 = new Student("Lily", "L", 1, GradeLevel.FirstYear, new List<int>() { 98, 99, 89 }, 1);
            var s2 = new Student("Lucy", "C", 2, GradeLevel.SecondYear, new List<int>() { 88, 87, 79 }, 2);
            var s3 = new Student("Linda", "L", 1, GradeLevel.FirstYear, new List<int>() { 90, 89, 85 }, 1);
            var s4 = new Student("Amy", "C", 2, GradeLevel.SecondYear, new List<int>() { 76, 67, 72 }, 2);
            var s5 = new Student("Amy", "A", 2, GradeLevel.SecondYear, new List<int>() { 80, 75, 83 }, 2);

            var students = new List<Student> { s1, s2, s3, s4, s5 };

            Console.WriteLine("分组是 LINQ 最强大的功能之一。 以下示例演示如何以各种方式对数据进行分组：");
            {

                Console.WriteLine("按单个属性分组示例");
                var groupByYearQuery =
                from student in students
                group student by student.Year into newGroup
                orderby newGroup.Key
                select newGroup;

                foreach (var yearGroup in groupByYearQuery)
                {
                    Console.WriteLine($"Key: {yearGroup.Key}");
                    foreach (var student in yearGroup)
                    {
                        Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
                    }
                }
            }

            {
                Console.WriteLine("以下示例显示了使用方法语法的等效代码");
                // Variable groupByLastNamesQuery is an IEnumerable<IGrouping<string,
                // DataClass.Student>>.
                var groupByYearQuery = students
                    .GroupBy(student => student.Year)
                    .OrderBy(newGroup => newGroup.Key);

                foreach (var yearGroup in groupByYearQuery)
                {
                    Console.WriteLine($"Key: {yearGroup.Key}");
                    foreach (var student in yearGroup)
                    {
                        Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
                    }
                }
            }


            Console.WriteLine("按值分组示例");
            {
                var groupByFirstLetterQuery =
                from student in students
                let firstLetter = student.LastName[0]
                group student by firstLetter;

                foreach (var studentGroup in groupByFirstLetterQuery)
                {
                    Console.WriteLine($"Key: {studentGroup.Key}");
                    foreach (var student in studentGroup)
                    {
                        Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
                    }
                }
            }

            {
                Console.WriteLine("以下示例显示了使用方法语法的等效代码");
                var groupByFirstLetterQuery = students
            .GroupBy(student => student.LastName[0]);

                foreach (var studentGroup in groupByFirstLetterQuery)
                {
                    Console.WriteLine($"Key: {studentGroup.Key}");
                    foreach (var student in studentGroup)
                    {
                        Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
                    }
                }
            }


            Console.WriteLine("按范围分组示例");
            {

                static int GetPercentile(Student s)
                {
                    double avg = s.Scores.Average();
                    return avg > 0 ? (int)avg / 10 : 0;
                }

                var groupByPercentileQuery =
                    from student in students
                    let percentile = GetPercentile(student)
                    group new
                    {
                        student.FirstName,
                        student.LastName
                    } by percentile into percentGroup
                    orderby percentGroup.Key
                    select percentGroup;

                foreach (var studentGroup in groupByPercentileQuery)
                {
                    Console.WriteLine($"Key: {studentGroup.Key * 10}");
                    foreach (var item in studentGroup)
                    {
                        Console.WriteLine($"\t{item.LastName}, {item.FirstName}");
                    }
                }
            }

            {
                Console.WriteLine("需要嵌套 foreach 才能循环访问组和组项。 以下示例显示了使用方法语法的等效代码");

                static int GetPercentile(Student s)
                {
                    double avg = s.Scores.Average();
                    return avg > 0 ? (int)avg / 10 : 0;
                }

                var groupByPercentileQuery = students
                    .Select(student => new { student, percentile = GetPercentile(student) })
                    .GroupBy(student => student.percentile)
                    .Select(percentGroup => new
                    {
                        percentGroup.Key,
                        Students = percentGroup.Select(s => new { s.student.FirstName, s.student.LastName })
                    })
                    .OrderBy(percentGroup => percentGroup.Key);

                foreach (var studentGroup in groupByPercentileQuery)
                {
                    Console.WriteLine($"Key: {studentGroup.Key * 10}");
                    foreach (var item in studentGroup.Students)
                    {
                        Console.WriteLine($"\t{item.LastName}, {item.FirstName}");
                    }
                }
            }


            Console.WriteLine("按比较分组示例");
            {
                var groupByHighAverageQuery =
                from student in students
                group new
                {
                    student.FirstName,
                    student.LastName
                } by student.Scores.Average() > 75 into studentGroup
                select studentGroup;

                foreach (var studentGroup in groupByHighAverageQuery)
                {
                    Console.WriteLine($"Key: {studentGroup.Key}");
                    foreach (var student in studentGroup)
                    {
                        Console.WriteLine($"\t{student.FirstName} {student.LastName}");
                    }
                }
            }

            {
                Console.WriteLine("以下示例显示了使用方法语法的等效代码");
                var groupByHighAverageQuery = students
                .GroupBy(student => student.Scores.Average() > 75)
                .Select(group => new
                {
                    group.Key,
                    Students = group.AsEnumerable().Select(s => new { s.FirstName, s.LastName })
                });

                foreach (var studentGroup in groupByHighAverageQuery)
                {
                    Console.WriteLine($"Key: {studentGroup.Key}");
                    foreach (var student in studentGroup.Students)
                    {
                        Console.WriteLine($"\t{student.FirstName} {student.LastName}");
                    }
                }
            }


            {
                //按匿名类型分组
                var groupByCompoundKey =
                from student in students
                group student by new
                {
                    FirstLetterOfLastName = student.LastName[0],
                    IsScoreOver85 = student.Scores[0] > 85
                } into studentGroup
                orderby studentGroup.Key.FirstLetterOfLastName
                select studentGroup;

                foreach (var scoreGroup in groupByCompoundKey)
                {
                    var s = scoreGroup.Key.IsScoreOver85 ? "more than 85" : "less than 85";
                    Console.WriteLine($"Name starts with {scoreGroup.Key.FirstLetterOfLastName} who scored {s}");
                    foreach (var item in scoreGroup)
                    {
                        Console.WriteLine($"\t{item.FirstName} {item.LastName}");
                    }
                }
            }


            {
                Console.WriteLine("以下代码显示了使用方法语法的等效查询");
                var groupByCompoundKey = students
                .GroupBy(student => new
                {
                    FirstLetterOfLastName = student.LastName[0],
                    IsScoreOver85 = student.Scores[0] > 85
                })
                .OrderBy(studentGroup => studentGroup.Key.FirstLetterOfLastName);

                foreach (var scoreGroup in groupByCompoundKey)
                {
                    var s = scoreGroup.Key.IsScoreOver85 ? "more than 85" : "less than 85";
                    Console.WriteLine($"Name starts with {scoreGroup.Key.FirstLetterOfLastName} who scored {s}");
                    foreach (var item in scoreGroup)
                    {
                        Console.WriteLine($"\t{item.FirstName} {item.LastName}");
                    }
                }
            }


            Console.WriteLine(" 创建嵌套组");
            {
                var nestedGroupsQuery =
                from student in students
                group student by student.Year into newGroup1
                from newGroup2 in
                from student in newGroup1
                group student by student.LastName
                group newGroup2 by newGroup1.Key;

                foreach (var outerGroup in nestedGroupsQuery)
                {
                    Console.WriteLine($"DataClass.Student Level = {outerGroup.Key}");
                    foreach (var innerGroup in outerGroup)
                    {
                        Console.WriteLine($"\tNames that begin with: {innerGroup.Key}");
                        foreach (var innerGroupElement in innerGroup)
                        {
                            Console.WriteLine($"\t\t{innerGroupElement.LastName} {innerGroupElement.FirstName}");
                        }
                    }
                }
            }

            {
                Console.WriteLine("以下代码显示了使用方法语法的等效查询");
                var nestedGroupsQuery =
                students
                .GroupBy(student => student.Year)
                .Select(newGroup1 => new
                {
                    newGroup1.Key,
                    NestedGroup = newGroup1
                        .GroupBy(student => student.LastName)
                });

                foreach (var outerGroup in nestedGroupsQuery)
                {
                    Console.WriteLine($"DataClass.Student Level = {outerGroup.Key}");
                    foreach (var innerGroup in outerGroup.NestedGroup)
                    {
                        Console.WriteLine($"\tNames that begin with: {innerGroup.Key}");
                        foreach (var innerGroupElement in innerGroup)
                        {
                            Console.WriteLine($"\t\t{innerGroupElement.LastName} {innerGroupElement.FirstName}");
                        }
                    }
                }
            }


            Console.WriteLine("对分组操作执行子查询");
            {
                var queryGroupMax =
                from student in students
                group student by student.Year into studentGroup
                select new
                {
                    Level = studentGroup.Key,
                    HighestScore = (
                        from student2 in studentGroup
                        select student2.Scores.Average()
                    ).Max()
                };

                var count = queryGroupMax.Count();
                Console.WriteLine($"Number of groups = {count}");

                foreach (var item in queryGroupMax)
                {
                    Console.WriteLine($"  {item.Level} Highest Score={item.HighestScore}");
                }
            }

            {
                Console.WriteLine("以下示例显示了使用方法语法的等效代码");
                var queryGroupMax =
                students
                    .GroupBy(student => student.Year)
                    .Select(studentGroup => new
                    {
                        Level = studentGroup.Key,
                        HighestScore = studentGroup.Max(student2 => student2.Scores.Average())
                    });

                var count = queryGroupMax.Count();
                Console.WriteLine($"Number of groups = {count}");

                foreach (var item in queryGroupMax)
                {
                    Console.WriteLine($"  {item.Level} Highest Score={item.HighestScore}");
                }
            }

        }
    }
}
