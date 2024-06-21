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
        public int OutOrderHeaderID { get; set; }

        /// <summary>
        ///主题
        /// </summary>
        [Display(Name = "主题")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(50)")]
        [Required(AllowEmptyStrings = false)]
        public string Theme { get; set; }

        /// <summary>
        ///测试申请单号
        /// </summary>
        [Display(Name = "测试申请单号")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        [Required(AllowEmptyStrings = false)]
        public string OutOrderHeaderNo { get; set; }

        /// <summary>
        ///申请日期
        /// </summary>
        [Display(Name = "申请日期")]
        [Column(TypeName = "datetime")]
        public DateTime? OrderDate { get; set; }

        /// <summary>
        ///申请人主键
        /// </summary>
        [Display(Name = "申请人主键")]
        [Column(TypeName = "int")]
        public int ApplicantId { get; set; }

        /// <summary>
        ///申请人
        /// </summary>
        [Display(Name = "申请人")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(50)")]
        public string Applicant { get; set; }

        /// <summary>
        ///申请部门
        /// </summary>
        [Display(Name = "申请部门")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(50)")]
        public string ApplicantDep { get; set; }

        /// <summary>
        ///项目类型
        /// </summary>
        [Display(Name = "项目类型")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(50)")]
        [Required(AllowEmptyStrings = false)]
        public string OrderType { get; set; }


        /// <summary>
        ///WBS编码
        /// </summary>
        [Display(Name = "WBS编码")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(50)")]
        public string ProjectCode { get; set; }

        /// <summary>
        ///项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(50)")]
        public string ProjectName { get; set; }


        /// <summary>
        ///测试类别
        /// </summary>
        [Display(Name = "测试类别")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(50)")]
        public string TestClass { get; set; }


        /// <summary>
        ///测试周期
        /// </summary>
        [Display(Name = "测试周期")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string TaskPeriod { get; set; }

        /// <summary>
        ///测试目的
        /// </summary>
        [Display(Name = "测试目的")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
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
        [Display(Name = "样品大类")]
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
        ///样品类型
        /// </summary>
        [Display(Name = "样品类型")]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string SampleGroup { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
        [Display(Name = "样品规格")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleSpec { get; set; }

        /// <summary>
        ///样品阶段
        /// </summary>
        [Display(Name = "样品阶段")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleStage { get; set; }


        /// <summary>
        ///样品版本号
        /// </summary>
        [Display(Name = "样品版本号")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string SampleVersion { get; set; }


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
        ///样品描述
        /// </summary>
        [Display(Name = "样品描述")]
        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string SampleDescribe { get; set; }

        /// <summary>
        ///组别是否是手输
        /// </summary>
        [Display(Name = "组别是否是手输")]
        [Column(TypeName = "bit")]
        public bool? IsProductCodeHand { get; set; }


        /// <summary>
        ///样品数量
        /// </summary>
        [Display(Name = "样品数量")]
        [Column(TypeName = "int")]
        public int? Quantity { get; set; }



        /// <summary>
        ///需求类型
        /// </summary>
        [Display(Name = "需求类型")]
        [MaxLength(100)]
        [Column(TypeName = "int")]
        public int RequirementType { get; set; }

      
      
        /// <summary>
        ///检毕样品计划处理方式
        /// </summary>
        [Display(Name = "检毕样品计划处理方式")]
        [MaxLength(100)]
        [Column(TypeName = "int")]
        public int PlanningMethod { get; set; }


        #endregion

        #region 样品特殊属性
        /// <summary>
        ///样品系列
        /// </summary>
        [Display(Name = "样品系列")]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SampleSeries { get; set; }

        /// <summary>
        ///样品状态
        /// </summary>
        [Display(Name = "样品状态")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string SampleStatus { get; set; }

        /// <summary>
        ///有潜在异常风险
        /// </summary>
        [Display(Name = "有潜在异常风险？")]
        [Column(TypeName = "bit")]
        public bool IsPotentialRisk { get; set; } = false;

        /// <summary>
        ///潜在异常风险描述
        /// </summary>
        [Display(Name = "潜在异常风险描述")]
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string PotentialRiskRemark { get; set; }


        /// <summary>
        ///样品SOC
        /// </summary>
        [Display(Name = "样品SOC")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string SampleSOC { get; set; }

        /// <summary>
        ///样品SOC描述
        /// </summary>
        [Display(Name = "样品SOC描述")]
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string SampleSOCRemark { get; set; }

        /// <summary>
        ///夹具
        /// </summary>
        [Display(Name = "夹具？")]
        [Column(TypeName = "bit")]
        public bool IsFixture { get; set; } = false;

        /// <summary>
        ///夹具
        /// </summary>
        [Display(Name = "夹具")]
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string Fixture { get; set; }


        /// <summary>
        ///工装
        /// </summary>
        [Display(Name = "夹具？")]
        [Column(TypeName = "bit")]
        public bool IsWorkwear { get; set; } = false;

        /// <summary>
        ///工装
        /// </summary>
        [Display(Name = "夹具")]
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string Workwear { get; set; }

        /// <summary>
        ///线束
        /// </summary>
        [Display(Name = "夹具？")]
        [Column(TypeName = "bit")]
        public bool IsWireHarness { get; set; } = false;

        /// <summary>
        ///线束
        /// </summary>
        [Display(Name = "线束")]
        [MaxLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string WireHarness { get; set; }



        #endregion

        #region OA
        /// <summary>
        ///订单关闭时间
        /// </summary>
        [Display(Name = "预估费用")]
        [Column(TypeName = "estimatedCost")]
        public decimal? EstimatedCost { get; set; }
       
        /// <summary>
        ///订单关闭时间
        /// </summary>
        [Display(Name = "实际费用")]
        [Column(TypeName = "actualExpenses")]
        public decimal? ActualExpenses { get; set; }

        ///// <summary>
        /////检毕样品实际处理方式
        ///// </summary>
        //[Display(Name = "检毕样品实际处理方式")]
        //[Column(TypeName = "actualMethod")]
        //public string ActualMethod { get; set; }

        ///// <summary>
        /////预计开始时间
        ///// </summary>
        //[Display(Name = "预计开始时间")]
        //[Column(TypeName = "estimatedStartTime")]
        //public DateTime? EstimatedStartTime { get; set; }

        ///// <summary>
        /////第三方评估
        ///// </summary>
        //[Display(Name = "thirdPartyEvaluation")]
        //[Column(TypeName = "thirdPartyEvaluation")]
        //public string thirdPartyEvaluation { get; set; }

        #endregion


        /// <summary>
        ///附件
        /// </summary>
        [Display(Name = "附件")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string Attachment { get; set; }


        /// <summary>
        ///订单状态
        /// </summary>
        [Display(Name = "订单状态")]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public OUT_ORDER_STATUS OrderStatus { get; set; } = OUT_ORDER_STATUS.Draft;


        /// <summary>
        ///订单关闭时间
        /// </summary>
        [Display(Name = "订单关闭时间")]
        [Column(TypeName = "datetime")]
        public DateTime? OrderCloseDate { get; set; }


        /// <summary>
        ///是否草稿
        /// </summary>
        [Display(Name = "是否草稿")]
        [Column(TypeName = "bit")]
        public bool? DraftStatus { get; set; }


        /// <summary>
        ///审批备注
        /// </summary>
        [Display(Name = "审批备注")]
        [MaxLength(1000)]
        [Column(TypeName = "nvarchar(1000)")]
        public string AuditRemark { get; set; }


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

        /// <summary>
        ///是否可送样
        /// </summary>
        [Display(Name = "是否可送样")]
        [Column(TypeName = "bit")]
        public bool? IsSendSamples { get; set; }


        ///////////////////////////////////////////////////

        ///// <summary>
        /////测试项目
        ///// </summary>
        //[Display(Name = "测试项目")]
        //[MaxLength(100)]
        //[Column(TypeName = "nvarchar(100)")]
        //public string TaskProject { get; set; }


        ///// <summary>
        /////测试状态
        ///// </summary>
        //[Display(Name = "测试状态")]
        //[MaxLength(50)]
        //[Column(TypeName = "nvarchar(50)")]
        //public string TestStatus { get; set; }

        ///// <summary>
        /////已收样数量
        ///// </summary>
        //[Display(Name = "已收样数量")]
        //[Column(TypeName = "int")]
        //public int? ReceivedQuantity { get; set; }     
    }
}