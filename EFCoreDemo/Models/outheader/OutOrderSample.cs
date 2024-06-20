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

namespace EFCoreDemo.Models
{
    public partial class OutOrderSample 
    {
        /// <summary>
        ///申请单样品主键
        /// </summary>
        [Key]
        [Display(Name = "申请单样品主键")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int ID { get; set; }

        /// <summary>
        ///申请单主键
        /// </summary>
        [Display(Name = "申请单主键")]
        [Column(TypeName = "int")]
        public int? OutOrderHeaderID { get; set; }
       
        /// <summary>
        ///申请单编号
        /// </summary>
        [Display(Name = "申请单编号")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string OutOrderHeaderNo { get; set; }

        /// <summary>
        ///生产条码
        /// </summary>
        [Display(Name = "生产条码")]
        [MaxLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string BarCode { get; set; }


        /// <summary>
        ///产品分类主键
        /// </summary>
        [Display(Name = "产品分类主键")]
        [Column(TypeName = "int")]
        public int? ProductClassId { get; set; }

        /// <summary>
        ///样品类型
        /// </summary>
        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleType { get; set; }

        /// <summary>
        ///样品描述/Package号/产品PN
        /// </summary>
        [Display(Name = "样品描述/Package号/产品PN")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string SampleDescribe { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
        [Display(Name = "样品规格")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleSpec { get; set; }

        /// <summary>
        ///测试编号
        /// </summary>
        [Display(Name = "测试编号")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string TestNo { get; set; }

        /// <summary>
        ///测试类型名称
        /// </summary>
        [Display(Name = "测试类型名称")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string TaskTypeName { get; set; }

        /// <summary>
        ///测试类型
        /// </summary>
        [Display(Name = "测试类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string TaskType { get; set; }

        /// <summary>
        ///组别
        /// </summary>
        [Display(Name = "组别")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductCode { get; set; }

        /// <summary>
        ///额定功率
        /// </summary>
        [Display(Name = "额定功率")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        public decimal? RatedPower { get; set; }

        /// <summary>
        ///额定容量
        /// </summary>
        [Display(Name = "额定容量")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        public decimal? RatedCapacity { get; set; }


        //SerialNo
        /// <summary>
        ///序号
        /// </summary>
        [Display(Name = "序号")]
        [Column(TypeName = "int")]
        public int? SerialNo { get; set; }

        /// <summary>
        ///Vmax
        /// </summary>
        [Display(Name = "Vmax")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        public decimal? Vmax { get; set; }

        /// <summary>
        ///Vmin
        /// </summary>
        [Display(Name = "Vmin")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        public decimal? Vmin { get; set; }

        public virtual OutOrderHeader OutOrderHeader { get; set; }
    }
}