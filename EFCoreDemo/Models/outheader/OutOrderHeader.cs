/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EFCoreDemo.Models
{    
    public partial class OutOrderHeader
    {
        #region 基本信息
        /// <summary>
        ///测试申请单主键
        /// </summary>
        [Key]
        [Display(Name = "测试申请单主键")]
        [Column(TypeName = "int")]
        [Required(AllowEmptyStrings = false)]
        [Comment("测试申请单主键")]
        public int ID { get; set; }

        /// <summary>
        ///主题
        /// </summary>
        [Display(Name = "主题")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Required(AllowEmptyStrings = false)]
        [Comment("主题")]
        public string Theme { get; set; }

        /// <summary>
        ///测试申请单号
        /// </summary>
        [Display(Name = "测试申请单号")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Required(AllowEmptyStrings = false)]
        [Comment("测试申请单号")]
        public string OutOrderHeaderNo { get; set; }

        /// <summary>
        ///申请日期
        /// </summary>
        [Display(Name = "申请日期")]
        [Column(TypeName = "datetime")]
        [Comment("申请日期")]
        public DateTime? OrderDate { get; set; }

        /// <summary>
        ///申请人主键
        /// </summary>
        [Display(Name = "申请人主键")]
        [Column(TypeName = "int")]
        [Comment("申请人主键")]
        public int? ApplicantId { get; set; }

        /// <summary>
        ///申请人
        /// </summary>
        [Display(Name = "申请人")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Comment("申请人")]
        public string Applicant { get; set; }

        /// <summary>
        ///申请部门
        /// </summary>
        [Display(Name = "申请部门")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Comment("申请部门")]
        public string ApplicantDep { get; set; }

        /// <summary>
        ///项目类型
        /// </summary>
        [Display(Name = "项目类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Required(AllowEmptyStrings = false)]
        [Comment("项目类型")]
        public string OrderType { get; set; }

        /// <summary>
        ///WBS编码
        /// </summary>
        [Display(Name = "WBS编码")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Comment("WBS编码")]
        public string ProjectCode { get; set; }

        /// <summary>
        ///项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Comment("项目名称")]
        public string ProjectName { get; set; }

        /// <summary>
        ///测试类别
        /// </summary>
        [Display(Name = "测试类别")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Comment("测试类别")]
        public string TestClass { get; set; }


        /// <summary>
        ///测试周期
        /// </summary>
        [Display(Name = "测试周期")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Comment("测试周期")]
        public string TaskPeriod { get; set; }

        /// <summary>
        ///测试目的
        /// </summary>
        [Display(Name = "测试目的")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        [Comment("测试目的")]
        public string TestPurposes { get; set; }

        /// <summary>
        ///必要性与价值
        /// </summary>
        [Display(Name = "必要性与价值")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string NecessityAndValue { get; set; }


        #endregion


        #region 样品信息

        /// <summary>
        ///样品类型
        /// </summary>
        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleType { get; set; }

        /// <summary>
        ///样品来源
        /// </summary>
        [Display(Name = "样品来源")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleSource { get; set; }

        /// <summary>
        ///需求类型
        /// </summary>
        [Display(Name = "需求类型")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string RequirementType { get; set; }

        /// <summary>
        ///样品类型
        /// </summary>
        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string SampleGroup { get; set; }


        /// <summary>
        ///样品阶段
        /// </summary>
        [Display(Name = "样品阶段")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleStage { get; set; }

        /// <summary>
        ///检毕样品计划处理方式
        /// </summary>
        [Display(Name = "检毕样品计划处理方式")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string PlanningMethod { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
        [Display(Name = "样品规格")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleSpec { get; set; }

        /// <summary>
        ///样品版本号
        /// </summary>
        [Display(Name = "样品版本号")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string SampleVersion { get; set; }


        /// <summary>
        ///样品数量
        /// </summary>
        [Display(Name = "样品数量")]
        [Column(TypeName = "int")]
        public int? Quantity { get; set; }


        /// <summary>
        ///样品描述
        /// </summary>
        [Display(Name = "样品描述")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string SampleDescribe { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "PackageId")]
        [Column(TypeName = "int")]
        public int? PackageId { get; set; }

        /// <summary>
        ///产品PN
        /// </summary>
        [Display(Name = "产品PN")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductPN { get; set; }


        /// <summary>
        ///样品规格
        /// </summary>
        [Display(Name = "样品规格")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleSpecName { get; set; }

        /// <summary>
        ///组别是否是手输
        /// </summary>
        [Display(Name = "组别是否是手输")]
        [Column(TypeName = "bit")]
        public bool? IsProductCodeHand { get; set; }


        /// <summary>
        ///订单关闭时间
        /// </summary>
        [Display(Name = "订单关闭时间")]
        [Column(TypeName = "datetime")]
        public DateTime? OrderCloseDate { get; set; }

        #endregion


        #region OA

        
        /// <summary>
        ///订单关闭时间
        /// </summary>
        [Display(Name = "预估费用")]
        [Column(TypeName = "decimal(18,3)")]
        [Comment("预估费用")]
        public decimal? EstimatedCost { get; set; }
       
        /// <summary>
        ///订单关闭时间
        /// </summary>
        [Display(Name = "实际费用")]
        [Column(TypeName = "decimal(18,3)")]
        public decimal? ActualExpenses { get; set; }

        /// <summary>
        ///检毕样品实际处理方式
        /// </summary>
        [Display(Name = "检毕样品实际处理方式")]      
        public string ActualMethod { get; set; }

        /// <summary>
        ///预计开始时间
        /// </summary>
        [Display(Name = "预计开始时间")]      
        public DateTime? EstimatedStartTime { get; set; }
      
        /// <summary>
        ///第三方评估
        /// </summary>
        [Display(Name = "nvarchar(500)")]       
        public string thirdPartyEvaluation { get; set; }

        #endregion


        /// <summary>
        ///是否草稿
        /// </summary>
        [Display(Name = "是否草稿")]
        [Column(TypeName = "bit")]
        public bool? DraftStatus { get; set; }

        /// <summary>
        ///订单状态
        /// </summary>
        [Display(Name = "订单状态")]
        [MaxLength(50)]
        [Column(TypeName = "int")]
        public OUT_ORDER_STATUS OrderStatus { get; set; } = OUT_ORDER_STATUS.Draft;


        /// <summary>
        ///附件
        /// </summary>
        [Display(Name = "附件")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string Attachment { get; set; }



        /// <summary>
        ///委外小组组内跟进人
        /// </summary>
        [Display(Name = "委外小组组内跟进人")]       
        [Column(TypeName = "int")]
        public int? FollowUpPersonID { get; set; }


        /// <summary>
        ///委外小组组内跟进人
        /// </summary>
        [Display(Name = "委外小组组内跟进人")]       
        [Column(TypeName = "nvarchar(200)")]
        public string FollowUpPersonName { get; set; }

        /// <summary>
        ///委外小组组内跟进人状态
        /// </summary>
        [Display(Name = "委外小组组内跟进人状态")]
        [Column(TypeName = "int")]
        public int FollowUpStatus { get; set; }
     

        /// <summary>
        ///研发状态
        /// </summary>
        [Display(Name = "研发状态")]
        [Column(TypeName = "int")]
        public int DevelopmentPersonStatus { get; set; }


        ///////////////////////////////////////////////////

        /// <summary>
        ///审批备注
        /// </summary>
        [Display(Name = "审批备注")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string AuditRemark { get; set; }


        /// <summary>
        ///测试项目
        /// </summary>
        [Display(Name = "测试项目")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string TaskProject { get; set; }


        /// <summary>
        ///测试状态
        /// </summary>
        [Display(Name = "测试状态")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string TestStatus { get; set; }

        /// <summary>
        ///已收样数量
        /// </summary>
        [Display(Name = "已收样数量")]
        [Column(TypeName = "int")]
        public int? ReceivedQuantity { get; set; }

       
        /// <summary>
        ///样品类型
        /// </summary>
        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleTypeName { get; set; }



        /// <summary>
        ///OA唯一识别信息
        /// </summary>
        [Display(Name = "OA唯一识别信息")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string OAUniqueIdentification { get; set; }


        /// <summary>
        ///OA审批通过时间
        /// </summary>
        [Display(Name = "OA审批通过时间")]
        [Column(TypeName = "datetime")]
        public DateTime? OAApprovedDate { get; set; }

        public ICollection<OutOrderDetail> OutOrderDetails { get; set; }
     
        public virtual ICollection<OutOrderSample> OutOrderSamples { get; set; }
    }
    public enum OUT_ORDER_STATUS
    {
        None = -1,
        [Description("草稿")]
        Draft = 0,
        [Description("待提交")]
        WaitSubmit = 1,
        [Description("已提交")]
        Submitted = 2,
        [Description("审批中")]
        Ongoing = 3,
        [Description("已通过")]
        Pass = 4,
        [Description("已驳回")]
        FailedPass = 5,
        [Description("在测")]
        Checking = 6,
        [Description("完成")]
        Finished = 7,
        [Description("修改申请单")]
        Edit = 8,
    }
}