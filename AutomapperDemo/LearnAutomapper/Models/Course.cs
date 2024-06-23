using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutomapper.Models
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Course
    {
        [Key]
        //[Comment("编号")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Range(0, 10)]
        public int? Credits { get; set; }

        public bool IsDeleted { get; set; } = false;

        //public ICollection<Enrollment> Enrollments { get; set; }
        public List<CourseInstructor> Instructors { get; set; }
    }

    /// <summary>
    /// 教师
    /// </summary>
    public class CourseInstructor
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstMidName { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public int CourseID { get; set; }

        public Course Course { get; set; } = new Course();

        public List<CourseInstructorDepartment> Departments { get; set; }

        //public OfficeAssignment? officeAssignment { get; set; }
    }

    /// <summary>
    /// 部门
    /// </summary>
    public class CourseInstructorDepartment
    {
        [Key]
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                       ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        //[NotMapped]
        //[Display(Name = "主任")]
        //public int? InstructorID { get; set; }
        //[NotMapped]
        //public Instructor Administrator { get; set; }

        //[Display(Name = "所属该部门的老师")]
        //public ICollection<Instructor> Instructors { get; set; }
    }
}
