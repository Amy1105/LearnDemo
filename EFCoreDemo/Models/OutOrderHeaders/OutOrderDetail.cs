/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EFCoreDemo.Models.OutOrderHeaders
{  
    public partial class OutOrderDetail 
    {
        /// <summary>
        ///测试任务主键
        /// </summary>
        [Key]
        [Display(Name = "测试任务主键")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int OutOrderDetailID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "OutOrderHeaderID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int OutOrderHeaderID { get; set; }

        /// <summary>
        ///测试任务行号
        /// </summary>
        [Display(Name = "测试任务行号")]
        [Column(TypeName = "int")]
        public int OrderLineNo { get; set; } = 0;

        /// <summary>
        ///测试类型
        /// </summary>
        [Display(Name = "测试类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public int TaskType { get; set; }


        /// <summary>
        ///测试类型value
        /// </summary>
        [Display(Name = "测试类型value")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string TaskTypeName { get; set; }

        /// <summary>
        ///测试项目
        /// </summary>
        [Display(Name = "测试项目")]
        [MaxLength(100)]
        [Column(TypeName = "int")]
        public int TaskProject { get; set; }

        /// <summary>
        ///测试项目Value
        /// </summary>
        [Display(Name = "测试项目Value")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string TaskProjectName { get; set; }


    }
}