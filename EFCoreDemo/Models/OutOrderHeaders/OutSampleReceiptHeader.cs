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
    public partial class OutSampleReceiptHeader
    {
        /// <summary>
       ///收样单主键
       /// </summary>
       [Key]
       [Display(Name ="收样单主键")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int OutSampleReceiptHeaderId { get; set; }

       /// <summary>
       ///备注
       /// </summary>
       [Display(Name ="备注")]
       [MaxLength(1000)]
       [Column(TypeName="nvarchar(1000)")]
       public string Remark { get; set; }

       /// <summary>
       ///收样人员
       /// </summary>
       [Display(Name ="收样人员")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string Recipient { get; set; }

       /// <summary>
       ///状态
       /// </summary>
       [Display(Name ="状态")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string Status { get; set; }

       /// <summary>
       ///已收样数量
       /// </summary>
       [Display(Name ="已收样数量")]
       [Column(TypeName="int")]
       public int? ReceivedQuantity { get; set; }

       /// <summary>
       ///样品总数量
       /// </summary>
       [Display(Name ="样品总数量")]
       [Column(TypeName="int")]
       public int? SampleQuantity { get; set; }

       /// <summary>
       ///收样时间
       /// </summary>
       [Display(Name ="收样时间")]
       [Column(TypeName="datetime")]
       public DateTime? ReceiptTime { get; set; }

       /// <summary>
       ///收样单号
       /// </summary>
       [Display(Name ="收样单号")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string OutSampleReceiptNo { get; set; }

       /// <summary>
       ///送样人员
       /// </summary>
       [Display(Name ="送样人员")]
       [MaxLength(50)]
       [Column(TypeName="nvarchar(50)")]
       public string Sender { get; set; }

       /// <summary>
       ///生成样品相关工单工序工步
       /// </summary>
       [Display(Name ="生成样品相关工单工序工步")]
       [Column(TypeName="bit")]
       public bool? IsWipOrderCreated { get; set; }

        /// <summary>
        ///区域ID
        /// </summary>
        [Display(Name = "区域ID")]
        [Column(TypeName = "int")]
        public int? AreaID { get; set; }


    }
}