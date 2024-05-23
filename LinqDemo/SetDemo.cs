using LinqDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    public class SetDemo
    {
        public static void Demo()
        {
            Console.WriteLine("---Distinct-删除集合中的重复值---");
            {
                string[] words = ["the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog"];

                IEnumerable<string> query = from word in words.Distinct()
                                            select word;
                foreach (var str in query)
                {
                    Console.WriteLine(str);
                }
            }
            Console.WriteLine("---DistinctBy---");

            //DistinctBy 是 Distinct 的替代方法，它采用 keySelector。 keySelector 用作源类型的比较鉴别器。
            //在以下代码中，单词根据其 Length 进行区分，并且显示每个长度的第一个单词：
            {
                string[] words = ["the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog"];

                foreach (string word in words.DistinctBy(p => p.Length))   //可以写expression
                {
                    Console.WriteLine(word);
                }
            }

            Console.WriteLine("---Except-返回差集---");
            {
                string[] words1 = ["the", "quick", "brown", "fox"];
                string[] words2 = ["jumped", "over", "the", "lazy", "dog"];

                IEnumerable<string> query = from word in words1.Except(words2)
                                            select word;

                foreach (var str in query)
                {
                    Console.WriteLine(str);
                }
            }

            //required  修饰属性的作用是？

            var t1 = new Teacher("Amy", "A", 921, "Londan");
            var t2 = new Teacher("Lily", "L", 965, "Paris");
            var teachers = new List<Teacher> { t1, t2 };

            var s1 = new Student("Lily", "L", 1, GradeLevel.FirstYear, new List<int>() { 98, 99, 89 }, 1);
            var s2 = new Student("Lucy", "C", 2, GradeLevel.SecondYear, new List<int>() { 88, 87, 79 }, 2);
            var students = new List<Student> { s1, s2 };

            Console.WriteLine("---ExceptBy-返回差集---");
            {
                int[] teachersToExclude = [
                901,    // English
    965,    // Mathematics
    932,    // Engineering
    945,    // Economics
    987,    // Physics
    901     // Chemistry
            ];

                foreach (Teacher teacher in teachers.ExceptBy(
                        teachersToExclude, teacher => teacher.ID))
                {
                    Console.WriteLine($"{teacher.First} {teacher.Last}");
                }
            }


            Console.WriteLine("---Intersect-返回交集---");
            {
                string[] words1 = ["the", "quick", "brown", "fox"];
                string[] words2 = ["jumped", "over", "the", "lazy", "dog"];

                IEnumerable<string> query = from word in words1.Intersect(words2)
                                            select word;

                foreach (var str in query)
                {
                    Console.WriteLine(str);
                }
            }
            Console.WriteLine("---IntersectBy-返回交集---");

            {
                foreach (Student person in
                students.IntersectBy(
                    teachers.Select(t => (t.First, t.Last)), s => (s.FirstName, s.LastName)))
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }

            Console.WriteLine("---Union-返回并集---");
            {

                string[] words1 = ["the", "quick", "brown", "fox"];
                string[] words2 = ["jumped", "over", "the", "lazy", "dog"];

                IEnumerable<string> query = from word in words1.Union(words2)
                                            select word;

                foreach (var str in query)
                {
                    Console.WriteLine(str);
                }
            }
            Console.WriteLine("---UnionBy-返回并集---");
            {
                foreach (var person in students.Select(s => (s.FirstName, s.LastName)).UnionBy(
                    teachers.Select(t => (FirstName: t.First, LastName: t.Last)), s => (s.FirstName, s.LastName)))
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }
    }
}
