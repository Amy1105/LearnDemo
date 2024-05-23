using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo.Models
{
    public enum GradeLevel
    {
        FirstYear = 1,
        SecondYear,
        ThirdYear,
        FourthYear
    };

    public class Student
    {
        public Student(string firstName, string lastName, int iD, GradeLevel year, List<int> scores, int departmentID)
        {
            FirstName = firstName;
            LastName = lastName;
            ID = iD;
            Year = year;
            Scores = scores;
            DepartmentID = departmentID;
        }

        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  int ID { get; set; }
        public  GradeLevel Year { get; set; }
        public  List<int> Scores { get; set; }
        public  int DepartmentID { get; set; }
    }

    public class Teacher
    {
        public Teacher(string first, string last, int iD, string city)
        {
            First = first;
            Last = last;
            ID = iD;
            City = city;
        }

        public  string First { get; set; }
        public  string Last { get; set; }
        public  int ID { get; set; }
        public  string City { get; set; }
    }
    public class Department
    {
        public Department(string name, int iD, int teacherID)
        {
            Name = name;
            ID = iD;
            TeacherID = teacherID;
        }

        public  string Name { get; set; }
        public int ID { get; set; }
        public  int TeacherID { get; set; }
    }
}
