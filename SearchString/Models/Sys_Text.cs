/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOL.Entity.DomainModels
{
    public partial class Sys_Text
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "TextId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int TextId { get; set; }


        [Display(Name = "TextMainID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int TextMainID { get; set; }
      
        /// <summary>
        ///值
        /// </summary>
        [Display(Name = "值")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        [Editable(true)]
        public string Value { get; set; }

        /// <summary>
        ///语言
        /// </summary>
        [Display(Name = "语言")]
        [MaxLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string Lang { get; set; }

        /// <summary>
        ///类型
        /// </summary>
        [Display(Name = "类型")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        public string Type { get; set; }



        /// <summary>
        ///键
        /// </summary>
        [Display(Name = "键")]
        [NotMapped]
        public string Key { get; set; }


    }
}