using System.ComponentModel.DataAnnotations;

namespace EFCoreDemo.Models
{
    /// <summary>
    /// 办公室分配
    /// </summary>
    public class OfficeAssignment
    {
        [Key]
        public int OfficeAssignmentID { get; set; }

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public int InstructorID { get; set; }

        /// <summary>
        /// 所属老师
        /// </summary>
        public Instructor Instructor { get; set; }
    }
}
