/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOL.Entity.DomainModels
{
    public partial class Sys_Text_Main 
    {
        /// <summary>
        ///多语言主键
        /// </summary>
        [Key]
        [Display(Name = "多语言主键")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int TextMainID { get; set; }

        /// <summary>
        ///键
        /// </summary>
        [Display(Name = "键")]
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Key { get; set; }

        [Display(Name = "值")]
        [NotMapped]
        public string Values { get; set; }

        [ForeignKey("TextMainID")]
        public ICollection<Sys_Text> Sys_Texts { get; set; }
    }
}