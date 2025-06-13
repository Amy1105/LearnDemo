/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOL.Entity.DomainModels
{
    //[Entity(TableCnName = "工序参数",TableName = "ProcessTestPara",FolderName = "Bop")]
    public partial class ProcessTestPara
    {       
       [Key]
       [Display(Name ="ProcessTestParaId")]
       [Column(TypeName="int")]
       [Required(AllowEmptyStrings=false)]
       public int ProcessTestParaId { get; set; }

       /// <summary>
       ///工序
       /// </summary>
       [Display(Name ="工序")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int ProcessOperationId { get; set; }

       /// <summary>
       ///工步号
       /// </summary>
       [Display(Name ="工步号")]
       [Column(TypeName="int")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public int SequenceNo { get; set; }

       /// <summary>
       ///工步名称
       /// </summary>
       [Display(Name ="工步名称")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       [Required(AllowEmptyStrings=false)]
       public string Name { get; set; }

        /// <summary>
        /// 工步名称枚举值
        /// </summary>
        [Display(Name = "StepEnum")]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string StepEnum { get; set; }


        /// <summary>
        ///描述
        /// </summary>
        [Display(Name ="描述")]
       [MaxLength(3200)]
       [Column(TypeName="nvarchar(3200)")]
       [Editable(true)]
       public string Description { get; set; }

       /// <summary>
       ///截止时间
       /// </summary>
       [Display(Name ="截止时间")]
       [MaxLength(200)]
       [Column(TypeName="nvarchar(200)")]
       [Editable(true)]
       public string DurationSeconds { get; set; }

       /// <summary>
       ///截止电压
       /// </summary>
       [Display(Name ="截止电压")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string CutOffVoltage { get; set; }

       /// <summary>
       ///截止电流
       /// </summary>
       [Display(Name ="截止电流")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string CutOffCurrent { get; set; }

       /// <summary>
       ///截止容量
       /// </summary>
       [Display(Name ="截止容量")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string CutOffCapacity { get; set; }

       /// <summary>
       ///采样时间变量
       /// </summary>
       [Display(Name ="采样时间变量")]
       [DisplayFormat(DataFormatString="18,4")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? DelatTime { get; set; }

       /// <summary>
       ///备注
       /// </summary>
       [Display(Name ="备注")]
       [MaxLength(3200)]
       [Column(TypeName="nvarchar(3200)")]
       [Editable(true)]
       public string Remark { get; set; }

       /// <summary>
       ///温度
       /// </summary>
       [Display(Name ="温度")]
       [MaxLength(200)]
       [Column(TypeName="nvarchar(200)")]
       [Editable(true)]
       public string Temperature { get; set; }

       /// <summary>
       ///电压(V)
       /// </summary>
       [Display(Name ="电压(V)")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string Voltage { get; set; }

       /// <summary>
       ///电流(C)
       /// </summary>
       [Display(Name ="电流(C)")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string Current { get; set; }

       /// <summary>
       ///截止条件
       /// </summary>
       [Display(Name ="截止条件")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string EndingCondition { get; set; }

       /// <summary>
       ///采样率(s)
       /// </summary>
       [Display(Name ="采样率(s)")]
       [DisplayFormat(DataFormatString="18,4")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? SampleRate { get; set; }

        /// <summary>
        ///终止值
        /// </summary>
        [Display(Name = "终止值")]
        [Column(TypeName = "decimal")]
        [Editable(true)]
        public decimal? To { get; set; }

        /// <summary>
        ///起始值
        /// </summary>
        [Display(Name ="起始值")]
       [Column(TypeName="decimal")]
       [Editable(true)]
       public decimal? From { get; set; }
     

       /// <summary>
       ///测试功率
       /// </summary>
       [Display(Name ="测试功率")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string Power { get; set; }

       /// <summary>
       ///循环开始序号
       /// </summary>
       [Display(Name ="循环开始序号")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? RepeatFromSequenceNo { get; set; }

       /// <summary>
       ///循环次数
       /// </summary>
       [Display(Name ="循环次数")]
       [Column(TypeName="int")]
       [Editable(true)]
       public int? RepeatTimes { get; set; }

       /// <summary>
       ///模拟工况文件路径
       /// </summary>
       [Display(Name ="模拟工况文件路径")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string ConditionFileName { get; set; }
    

       /// <summary>
       ///水冷机
       /// </summary>
       [Display(Name ="水冷机")]
       [MaxLength(100)]
       [Column(TypeName="nvarchar(100)")]
       public string WaterCooler { get; set; }

       /// <summary>
       ///高级变量参数
       /// </summary>
       [Display(Name ="高级变量参数")]
       [MaxLength(200)]
       [Column(TypeName="nvarchar(200)")]
       [Editable(true)]
       public string VariableJson { get; set; }

       /// <summary>
       ///高级条件参数
       /// </summary>
       [Display(Name ="高级条件参数")]
       [MaxLength(800)]
       [Column(TypeName="nvarchar(800)")]
       [Editable(true)]
       public string ConditionJson { get; set; }

       /// <summary>
       ///温度截止
       /// </summary>
       [Display(Name = "温度截止")]
       [MaxLength(200)]
       [Column(TypeName = "nvarchar(200)")]
       [Editable(true)]
       public string CutTemperature { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "ProcessId")]
        [Column(TypeName = "int")]
        public int? ProcessId { get; set; }
    }
}