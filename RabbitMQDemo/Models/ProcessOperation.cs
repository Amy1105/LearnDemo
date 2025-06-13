/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOL.Entity.DomainModels
{
   // [Entity(TableCnName = "测试工序", TableName = "ProcessOperation", DetailTable = new Type[] { typeof(ProcessTestPara) }, FolderName = "Bop", DetailTableCnName = "工序参数表")]
    public partial class ProcessOperation 
    {      
        [Key]
        [Display(Name = "ProcessOperationId")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int ProcessOperationId { get; set; }

        /// <summary>
        ///测试流程
        /// </summary>
        [Display(Name = "测试流程")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? ProcessId { get; set; }

        /// <summary>
        ///工序名称
        /// </summary>
        [Display(Name = "工序名称")]
        [Column(TypeName = "int")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public int OperationId { get; set; }

        /// <summary>
        ///序号
        /// </summary>
        [Display(Name = "序号")]
        [Column(TypeName = "int")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public int OperationSequenceNo { get; set; }

        /// <summary>
        ///工序类型
        /// </summary>
        [Display(Name = "工序类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public string OperationType { get; set; }

        /// <summary>
        ///生效日期
        /// </summary>
        [Display(Name = "生效日期")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        ///失效日期
        /// </summary>
        [Display(Name = "失效日期")]
        [Column(TypeName = "datetime")]
        [Editable(true)]
        public DateTime? DiscontinueDate { get; set; }

        /// <summary>
        ///测试简码
        /// </summary>
        [Display(Name = "测试简码")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        [Required(AllowEmptyStrings = false)]
        [Editable(true)]
        public string Description { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        [Editable(true)]
        public string Remark { get; set; }

        /// <summary>
        ///工时(天)
        /// </summary>
        [Display(Name = "工时(天)")]
        [Column(TypeName = "numeric")]
        [Editable(true)]
        public decimal? OperationDay { get; set; }

        /// <summary>
        ///参考工时(天)
        /// </summary>
        [Display(Name = "参考工时(天)")]
        [Column(TypeName = "numeric")]
        [Editable(true)]
        public decimal? OperationDayOld { get; set; }

        /// <summary>
        ///测试类型
        /// </summary>
        [Display(Name = "测试类型")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        public string TestType { get; set; }

        /// <summary>
        ///工作中心
        /// </summary>
        [Display(Name = "工作中心")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        public string WorkCenter { get; set; }

        /// <summary>
        ///基础工序编码
        /// </summary>
        [Display(Name = "基础工序编码")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        public string OperationCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "OperationName")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string OperationName { get; set; }

        /// <summary>
        ///全局采样时间
        /// </summary>
        [Display(Name = "全局采样时间")]
        [DisplayFormat(DataFormatString = "18,3")]
        [Column(TypeName = "decimal")]
        public decimal? DefaultSamplingTime { get; set; }

        /// <summary>
        ///描述1(上个描述用作了测试简码)
        /// </summary>
        [Display(Name = "描述1(上个描述用作了测试简码)")]
        [MaxLength(1600)]
        [Column(TypeName = "nvarchar(1600)")]
        public string Description1 { get; set; }

        /// <summary>
        ///全局水冷机
        /// </summary>
        [Display(Name = "全局水冷机")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string WaterCooler { get; set; }


        [Display(Name = "测试简码温度")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public string TestCodeSerialTemperature { get; set; }


        [Display(Name = "测试简码序号")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public string TestCodeSerialNumber { get; set; }


        [Display(Name = "工序属性")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [Editable(true)]
        public string OperationAttributeEnum { get; set; }

        [ForeignKey("ProcessOperationId")]
        public ICollection<ProcessTestPara> ProcessTestParas { get; set; }
    }
}