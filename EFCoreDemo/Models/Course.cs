using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EFCoreDemo.Models
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Range(0, 10)]
        public int? Credits { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
}
