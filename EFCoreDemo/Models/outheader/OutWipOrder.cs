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
    public partial class OutWipOrder
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        [Display(Name = "ID")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int ID { get; set; }


        /// <summary>
        ///
        /// </summary>
        [Display(Name = "OrderLineNo")]
        [Column(TypeName = "int")]
        public int? OrderLineNo { get; set; }

        /// <summary>
        ///样品编号
        /// </summary>
        [Display(Name = "样品编号")]
        [MaxLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string TestNo { get; set; }

        /// <summary>
        ///电芯号
        /// </summary>
        [Display(Name = "电芯号")]
        [MaxLength(80)]
        [Column(TypeName = "nvarchar(80)")]
        public string BarCode { get; set; }


        /// <summary>
        ///工单状态
        /// </summary>
        [Display(Name = "工单状态")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string WorkOrderStatus { get; set; } = WIP_OUTORDER_STATUS.WaitChecking;


        /// <summary>
        ///样品组总数量
        /// </summary>
        [Display(Name = "样品组总数量")]
        [DisplayFormat(DataFormatString = "28,10")]
        [Column(TypeName = "decimal")]
        public decimal? OrderQuantity { get; set; }


        [Display(Name = "电芯ID")]
        [Column(TypeName = "int")]
        public int? OutOrderSampleId { get; set; }


        //组别


        /// <summary>
        ///优先级
        /// </summary>
        [Display(Name = "优先级")]
        [Column(TypeName = "int")]
        public int? Priority { get; set; }


        /// <summary>
        ///序号
        /// </summary>
        [Display(Name = "序号")]
        [Column(TypeName = "int")]
        public int? SerialNo { get; set; }


        /// <summary>
        ///顺序号
        /// </summary>
        [Display(Name = "顺序号")]
        [Column(TypeName = "int")]
        public int? WipSequenceNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "OrderHeaderId")]
        [Column(TypeName = "int")]
        public int? OutOrderHeaderID { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "OrderDetailId")]
        [Column(TypeName = "int")]
        public int? OutOrderDetailId { get; set; }

        /// <summary>
        ///产品主键
        /// </summary>
        [Display(Name = "产品主键")]
        [Column(TypeName = "int")]
        public int? ProductID { get; set; }


        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ProductCode")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductCode { get; set; }


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



        [Display(Name = "额定功率")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? RatedPower { get; set; }

        public virtual OutOrderDetail OutOrderDetail { get; set; }

        public virtual OutOrderHeader OutOrderHeader { get; set; }
    }

    public static class WIP_OUTORDER_STATUS
    {
        /// <summary>
        /// 初始状态：待测
        /// </summary>
        public static string WaitChecking = "待测";

        /// <summary>
        /// 第一个工序开始：在检
        /// </summary>
        public static string Checking = "在测";

        /// <summary>
        /// 全部工序完成：检毕
        /// </summary>
        public static string CheckFinish = "完成";

        /// <summary>
        /// 替换：终止
        /// </summary>
        public static string Ending = "终止";


        public static string Cancel = "取消测试";


        public static string OverdueScrap = "超期报废";


        public static string Scrap = "报废";
    }
}