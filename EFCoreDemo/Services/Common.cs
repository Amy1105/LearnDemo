﻿using EFCoreDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Services
{
    public class Common
    {
        /// <summary>
        /// 构建课程实体（联表）
        /// </summary>
        /// <returns></returns>
        public static List<Course> GetCourses(int Count)
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
                        FirstMidName = "bulkKim" + i.ToString() + "-" + j.ToString(),
                        LastName = "Abercrombie" + i.ToString() + "-" + j.ToString(),
                        HireDate = DateTime.Parse("1995-03-11"),
                    });
                }
                //这门课
                Courses.Add(new Course
                {
                    Title = "bulkLiterature" + i.ToString(),
                    Credits = i,
                    Instructors = instructors
                });
            }
            return Courses;
        }
    }
}