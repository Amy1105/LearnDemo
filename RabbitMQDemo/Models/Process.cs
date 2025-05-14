/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOL.Entity.DomainModels
{
    //[Entity(TableCnName = "测试流程", TableName = "Process", DetailTable = new Type[] { typeof(ProcessOperation) }, FolderName = "Bop", DetailTableCnName = "流程工序")]
    public partial class Process
    {
        /// <summary>
        ///测试流程主键
        /// </summary>
        [Key]
        [Display(Name = "测试流程主键")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        public int ProcessId { get; set; }

        /// <summary>
        ///流程类型主键
        /// </summary>
        [Display(Name = "流程类型主键")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? ProcessTypeId { get; set; }

        /// <summary>
        ///流程状态
        /// </summary>
        [Display(Name = "流程状态")]
        [Column(TypeName = "int")]
        public int? ProcessStatus { get; set; }

        /// <summary>
        ///流程类别主键
        /// </summary>
        [Display(Name = "流程类别主键")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? ProcessClassId { get; set; }

        /// <summary>
        ///是模板
        /// </summary>
        [Display(Name = "是模板")]
        [Column(TypeName = "bit")]
        public bool? IsTemplate { get; set; }

        /// <summary>
        ///项目主键
        /// </summary>
        [Display(Name = "项目主键")]
        [Column(TypeName = "int")]
        public int? ProjectID { get; set; }

        /// <summary>
        ///属于工厂
        /// </summary>
        [Display(Name = "属于工厂")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string OwnerFacility { get; set; }

        /// <summary>
        ///公司名称
        /// </summary>
        [Display(Name = "公司名称")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Company { get; set; }

        /// <summary>
        ///停止日期
        /// </summary>
        [Display(Name = "停止日期")]
        [Column(TypeName = "datetime")]
        public DateTime? DiscontinueDate { get; set; }

        /// <summary>
        ///生效日期
        /// </summary>
        [Display(Name = "生效日期")]
        [Column(TypeName = "datetime")]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        ///WBS编码
        /// </summary>
        [Display(Name = "WBS编码")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string WBSCode { get; set; }

        /// <summary>
        ///版本状态主键
        /// </summary>
        [Display(Name = "版本状态主键")]
        [Column(TypeName = "int")]
        public int? RevisionStatusID { get; set; }

        /// <summary>
        ///默认流程版本
        /// </summary>
        [Display(Name = "默认流程版本")]
        [Column(TypeName = "bit")]
        public bool? DefaultProcessRevision { get; set; }

        /// <summary>
        ///上级流程主键
        /// </summary>
        [Display(Name = "上级流程主键")]
        [Column(TypeName = "int")]
        public int? ParentProcessID { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string Remark { get; set; }

        /// <summary>
        ///测试内容
        /// </summary>
        [Display(Name = "测试内容")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        [Editable(true)]
        public string TestContent { get; set; }

        /// <summary>
        ///测试类别
        /// </summary>
        [Display(Name = "测试类别")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public string TestClass { get; set; }

        /// <summary>
        ///测试项目
        /// </summary>
        [Display(Name = "测试项目")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        public string TestItem { get; set; }

        /// <summary>
        ///测试类型
        /// </summary>
        [Display(Name = "测试类型")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        public string TestType { get; set; }

        /// <summary>
        ///测试依据
        /// </summary>
        [Display(Name = "测试依据")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string TestStandard { get; set; }

        /// <summary>
        ///流程描述
        /// </summary>
        [Display(Name = "流程描述")]
        [MaxLength(400)]
        [Column(TypeName = "nvarchar(400)")]
        public string ProcessDesc { get; set; }

        /// <summary>
        ///流程名称
        /// </summary>
        [Display(Name = "流程名称")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Editable(true)]
        [Required(AllowEmptyStrings = false)]
        public string ProcessName { get; set; }

        /// <summary>
        ///流程版本
        /// </summary>
        [Display(Name = "流程版本")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public string ProcessRevision { get; set; }

        /// <summary>
        ///启用
        /// </summary>
        [Display(Name = "启用")]
        [Column(TypeName = "bit")]
        [Editable(true)]
        public bool? Enable { get; set; }

        /// <summary>
        ///特殊温度
        /// </summary>
        [Display(Name = "特殊温度")]
        [Column(TypeName = "bit")]
        [Editable(true)]
        public bool? IsTemp { get; set; }

        /// <summary>
        ///常规要求
        /// </summary>
        [Display(Name = "常规要求")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string RuleAsk { get; set; }

        /// <summary>
        ///夹具要求
        /// </summary>
        [Display(Name = "夹具要求")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string FixtureAsk { get; set; }

        /// <summary>
        ///温控要求
        /// </summary>
        [Display(Name = "温控要求")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string TempAsk { get; set; }

        /// <summary>
        ///取点要求
        /// </summary>
        [Display(Name = "取点要求")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string GetPintAsk { get; set; }

        /// <summary>
        ///保护条件
        /// </summary>
        [Display(Name = "保护条件")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string ProtectionConditions { get; set; }

        /// <summary>
        ///其他要求
        /// </summary>
        [Display(Name = "其他要求")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string OtherAsk { get; set; }

        /// <summary>
        ///测试报告
        /// </summary>
        [Display(Name = "测试报告")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string TestReportAsk { get; set; }

        /// <summary>
        ///采点要求
        /// </summary>
        [Display(Name = "采点要求")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Editable(true)]
        public string PickPint { get; set; }


        [Display(Name = "工时")]
        [Column(TypeName = "int")]
        public int? BopWorkTime { get; set; }


        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public string SampleGroup { get; set; }


        [Display(Name = "老BopID")]
        [Column(TypeName = "int")]
        public int? OldProcessID { get; set; }


        [Display(Name = "BOP审批状态")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? BopStatus { get; set; }


        [Display(Name = "Bop版本")]
        [Column(TypeName = "int")]
        [Editable(true)]
        public int? Version { get; set; }


        [Display(Name = "审批流级别")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string FlowType { get; set; }


        [Display(Name = "当前审批人")]
        [Column(TypeName = "int")]
        public int? ExamineUserID { get; set; }


        [Display(Name = "当前提交人")]
        [Column(TypeName = "int")]
        public int? SubmitUserID { get; set; }

        [Display(Name = "一级审批人")]
        [Column(TypeName = "int")]
        public int? ExamineUserOneID { get; set; }


        [Display(Name = "二级审批人")]
        [Column(TypeName = "int")]
        public int? ExamineUserTwoID { get; set; }

        [Display(Name = "测试通过标准")]
        [Column(TypeName = "nvarchar(max)")]
        [Editable(true)]
        public string TestPassStandard { get; set; }

        [Display(Name = "项目类型")]
        [Column(TypeName = "nvarchar(100)")]
        [Editable(true)]
        public string ProjectType { get; set; }

        [Display(Name = "项目类型枚举值")]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string ProjectTypeCode { get; set; }


        [Display(Name = "项目属性")]
        [Column(TypeName = "nvarchar(50)")]
        [Editable(true)]
        public string ProjectAttribute { get; set; }


        [Display(Name = "测试类别")]
        [Column(TypeName = "varchar(50)")]
        [Editable(true)]
        public string TestClassEnum { get; set; }

        

        [Column(TypeName = "nvarchar(150)")]
        public string OtherProcessName { get; set; }


        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        [Editable(true)]
        public string SampleGroupCode { get; set; }

        [ForeignKey("ProcessId")]
        public List<ProcessOperation> ProcessOperations { get; set; }
    }
}