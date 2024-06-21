using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department
    {
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
