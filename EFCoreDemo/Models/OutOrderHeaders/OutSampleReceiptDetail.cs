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


namespace VOL.Entity.DomainModels
{
     public partial class OutSampleReceiptDetail
    {
       /// <summary>
       ///收样单明细主键
       /// </summary>
       [Key]
       [Display(Name ="收样单明细主键")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int OutSampleReceiptDetailId { get; set; }

       /// <summary>
       ///收样单主键
       /// </summary>
       [Display(Name ="收样单主键")]
       [Column(TypeName="int")]
       public int? OutSampleReceiptHeaderId { get; set; }

       /// <summary>
       ///样品编号
       /// </summary>
       [Display(Name ="样品编号")]
       [MaxLength(80)]
       [Column(TypeName="nvarchar(80)")]
       public string TestNo { get; set; }

       /// <summary>
       ///样品生产条码
       /// </summary>
       [Display(Name ="样品生产条码")]
       [MaxLength(80)]
       [Column(TypeName="nvarchar(80)")]
       public string BarCode { get; set; }

       /// <summary>
       ///样品测试编号
       /// </summary>
       [Display(Name ="样品测试编号")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string SampleNo { get; set; }

       /// <summary>
       ///数量
       /// </summary>
       [Display(Name ="数量")]
       [Column(TypeName="int")]
       public int? Quantity { get; set; }

       /// <summary>
       ///测试申请单号
       /// </summary>
       [Display(Name ="测试申请单号")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string OutOrderHeaderNo { get; set; }

       /// <summary>
       ///最大值
       /// </summary>
       [Display(Name ="最大值")]
       [DisplayFormat(DataFormatString="18,3")]
       [Column(TypeName="decimal")]
       public decimal? Vmax { get; set; }

       /// <summary>
       ///最小值
       /// </summary>
       [Display(Name ="最小值")]
       [DisplayFormat(DataFormatString="18,3")]
       [Column(TypeName="decimal")]
       public decimal? Vmin { get; set; }

       /// <summary>
       ///额定容量
       /// </summary>
       [Display(Name ="额定容量")]
       [DisplayFormat(DataFormatString="18,3")]
       [Column(TypeName="decimal")]
       public decimal? RatedCapacity { get; set; }

       /// <summary>
       ///样品规格
       /// </summary>
       [Display(Name ="样品规格")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string SampleSpec { get; set; }

       /// <summary>
       ///样品类型
       /// </summary>
       [Display(Name ="样品类型")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string SampleType { get; set; }

       /// <summary>
       ///测试类型
       /// </summary>
       [Display(Name ="测试类型")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string TaskType { get; set; }

       /// <summary>
       ///备注
       /// </summary>
       [Display(Name ="备注")]
       [MaxLength(1000)]
       [Column(TypeName="nvarchar(1000)")]
       public string Remark { get; set; }

       /// <summary>
       ///已收样
       /// </summary>
       [Display(Name ="已收样")]
       [Column(TypeName="bit")]
       public bool? IsReceipted { get; set; }

       /// <summary>
       ///收样明细状态
       /// </summary>
       [Display(Name ="收样明细状态")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string Status { get; set; }

       /// <summary>
       ///产品主键
       /// </summary>
       [Display(Name ="产品主键")]
       [Column(TypeName="int")]
       public int? ProductId { get; set; }

       /// <summary>
       ///测试申请人
       /// </summary>
       [Display(Name ="测试申请人")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string Applicant { get; set; }

       /// <summary>
       ///是否创建工序
       /// </summary>
       [Display(Name ="是否创建工序")]
       [Column(TypeName="bit")]
       public bool? IsWipOperationCreated { get; set; }

       /// <summary>
       ///申请单样品主键
       /// </summary>
       [Display(Name ="申请单样品主键")]
       [Column(TypeName="int")]
       public int? OutOrderSampleId { get; set; }

       /// <summary>
       ///申请单主键
       /// </summary>
       [Display(Name ="申请单主键")]
       [Column(TypeName="int")]
       public int? OutOrderHeaderId { get; set; }

       /// <summary>
       ///额定功率
       /// </summary>
       [Display(Name ="额定功率")]
       [DisplayFormat(DataFormatString="18,3")]
       [Column(TypeName="decimal")]
       public decimal? RatedPower { get; set; }

       /// <summary>
       ///来样检备注
       /// </summary>
       [Display(Name ="来样检备注")]
       [MaxLength(1000)]
       [Column(TypeName="nvarchar(1000)")]
       public string ReceiptCheckRemark { get; set; }

       /// <summary>
       ///收样实测电压
       /// </summary>
       [Display(Name ="收样实测电压")]
       [DisplayFormat(DataFormatString="18,3")]
       [Column(TypeName="decimal")]
       public decimal? ReceiptVoltage { get; set; }

       /// <summary>
       ///测试样品工单主键
       /// </summary>
       [Display(Name ="测试样品工单主键")]
       [Column(TypeName="int")]
       public int? OutWipOrderId { get; set; }

       
    }
}