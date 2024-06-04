using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
  
    public partial class SampleReceiptHeader 
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "SampleReceiptHeaderId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int SampleReceiptHeaderId { get; set; }

        /// <summary>
        ///收样单编号
        /// </summary>
        [Display(Name = "收样单编号")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleReceiptNo { get; set; }

        /// <summary>
        ///仓库
        /// </summary>
        [Display(Name = "仓库")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int WarehouseId { get; set; }

        /// <summary>
        ///收样时间
        /// </summary>
        [Display(Name = "收样时间")]
        [Column(TypeName = "datetime")]
        public DateTime? ReceiptTime { get; set; }

        /// <summary>
        ///样品总数量
        /// </summary>
        [Display(Name = "样品总数量")]
        [Column(TypeName = "int")]
        public int? SampleQuantity { get; set; }

        /// <summary>
        ///收样数量
        /// </summary>
        [Display(Name = "收样数量")]
        [Column(TypeName = "int")]
        public int? ReceivedQuantity { get; set; }

        /// <summary>
        ///状态
        /// </summary>
        [Display(Name = "状态")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string SampleStatus { get; set; }

        /// <summary>
        ///收样人员
        /// </summary>
        [Display(Name = "收样人员")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Recipient { get; set; }

        /// <summary>
        ///送样人员
        /// </summary>
        [Display(Name = "送样人员")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Sender { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string Remark { get; set; }

        /// <summary>
        ///生成样品相关工单工序工步
        /// </summary>
        [Display(Name = "生成样品相关工单工序工步")]
        [Column(TypeName = "bit")]
        public bool? IsWipOrderCreated { get; set; }

        [ForeignKey("SampleReceiptHeaderId")]
        public ICollection<SampleReceiptDetail> SampleReceiptDetails { get; set; }
    }

   
    public partial class SampleReceiptDetail
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "SampleReceiptDetailId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int SampleReceiptDetailId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "SampleReceiptHeaderId")]
        [Column(TypeName = "int")]
        public int? SampleReceiptHeaderId { get; set; }

        /// <summary>
        ///内部测试编号
        /// </summary>
        [Display(Name = "内部测试编号")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string SampleNo { get; set; }

        /// <summary>
        ///数量
        /// </summary>
        [Display(Name = "数量")]
        [Column(TypeName = "int")]
        public int? Quantity { get; set; }

        /// <summary>
        ///测试申请单号
        /// </summary>
        [Display(Name = "测试申请单号")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string OrderHerNo { get; set; }

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

        /// <summary>
        ///额定容量
        /// </summary>
        [Display(Name = "额定容量")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        public decimal? RatedCapacity { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
        [Display(Name = "样品规格")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleSpec { get; set; }

        /// <summary>
        ///样品类型
        /// </summary>
        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleType { get; set; }

        /// <summary>
        ///测试类型
        /// </summary>
        [Display(Name = "测试类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string TaskType { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string Remark { get; set; }

        /// <summary>
        ///是否收样
        /// </summary>
        [Display(Name = "是否收样")]
        [Column(TypeName = "bit")]
        public bool? IsReceipted { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "WipOrderId")]
        [Column(TypeName = "int")]
        public int? WipOrderId { get; set; }

        /// <summary>
        ///样品编号
        /// </summary>
        [Display(Name = "样品编号")]
        [MaxLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string TestNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "BarCode")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string BarCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "SampleDetailStatus")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string SampleDetailStatus { get; set; }

        /// <summary>
        ///产品主键
        /// </summary>
        [Display(Name = "产品主键")]
        [Column(TypeName = "int")]
        public int? ProductId { get; set; }

        /// <summary>
        ///测试申请人
        /// </summary>
        [Display(Name = "测试申请人")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Applicant { get; set; }

        /// <summary>
        ///是否创建工序
        /// </summary>
        [Display(Name = "是否创建工序")]
        [Column(TypeName = "bit")]
        public bool? IsWipOperationCreated { get; set; }

        /// <summary>
        ///申请单样品主键
        /// </summary>
        [Display(Name = "申请单样品主键")]
        [Column(TypeName = "int")]
        public int? OrderSampleId { get; set; }

        /// <summary>
        ///申请单主键
        /// </summary>
        [Display(Name = "申请单主键")]
        [Column(TypeName = "int")]
        public int? OrderHeaderId { get; set; }

        [Display(Name = "额定功率")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? RatedPower { get; set; }

    }
}
