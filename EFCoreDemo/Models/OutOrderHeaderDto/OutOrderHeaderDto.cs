using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VOL.Entity.DomainModels;
using System.ComponentModel;

namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    public class OutOrderHeaderDto
    {

        /// <summary>
        ///测试申请单主键
        /// </summary>
        public int? OutOrderHeaderID { get; set; }

        /// <summary>
        ///主题
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        ///申请日期
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        ///申请人主键
        /// </summary>
        public int ApplicantId { get; set; }

        /// <summary>
        ///申请人
        /// </summary>
        public string Applicant { get; set; }

        /// <summary>
        ///申请部门
        /// </summary>
        public string ApplicantDep { get; set; }

        /// <summary>
        ///项目类型
        /// </summary>
        public string OrderType { get; set; }


        /// <summary>
        ///WBS编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        ///项目名称
        /// </summary>
        public string ProjectName { get; set; }


        /// <summary>
        ///测试类别
        /// </summary>
        public string TestClass { get; set; }


        /// <summary>
        ///测试周期
        /// </summary>
        public string TaskPeriod { get; set; }

        /// <summary>
        ///测试目的
        /// </summary>
        public string TestPurposes { get; set; }

        /// <summary>
        ///必要性与价值
        /// </summary>
        public string NecessityAndValue { get; set; }


        /// <summary>
        ///样品类型
        /// </summary>
        public string SampleType { get; set; }

        /// <summary>
        ///样品来源
        /// </summary>
        public string SampleSource { get; set; }

        /// <summary>
        ///样品类型
        /// </summary>
        public string SampleGroup { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
        public string SampleSpec { get; set; }

        /// <summary>
        ///样品阶段
        /// </summary>
        public string SampleStage { get; set; }


        /// <summary>
        ///样品版本号
        /// </summary>
        public string SampleVersion { get; set; }


        /// <summary>
        ///
        /// </summary>
        public int? PackageId { get; set; }

        /// <summary>
        ///产品PN
        /// </summary>
        public string ProductPN { get; set; }

        /// <summary>
        ///样品描述
        /// </summary>
        public string SampleDescribe { get; set; }

        /// <summary>
        ///组别是否是手输
        /// </summary>
        public bool IsProductCodeHand { get; set; } = false;


        /// <summary>
        ///样品数量
        /// </summary>
        public int? Quantity { get; set; }



        /// <summary>
        ///需求类型
        /// </summary>
        public int RequirementType { get; set; }



        /// <summary>
        ///检毕样品计划处理方式
        /// </summary>
        public int PlanningMethod { get; set; }



        /// <summary>
        ///样品系列
        /// </summary>
        public string SampleSeries { get; set; }

        /// <summary>
        ///样品状态
        /// </summary>
        public string SampleStatus { get; set; }

        /// <summary>
        ///有潜在异常风险
        /// </summary>
        public bool IsPotentialRisk { get; set; } = false;

        /// <summary>
        ///潜在异常风险描述
        /// </summary>
        public string PotentialRiskRemark { get; set; }


        /// <summary>
        ///样品SOC
        /// </summary>
        public string SampleSOC { get; set; }

        /// <summary>
        ///样品SOC描述
        /// </summary>
        public string SampleSOCRemark { get; set; }

        /// <summary>
        ///夹具
        /// </summary>
        public bool IsFixture { get; set; } = false;

        /// <summary>
        ///夹具
        /// </summary>
        public string Fixture { get; set; }


        /// <summary>
        ///工装
        /// </summary>
        public bool IsWorkwear { get; set; } = false;

        /// <summary>
        ///工装
        /// </summary>
        public string Workwear { get; set; }

        /// <summary>
        ///线束
        /// </summary>
        public bool IsWireHarness { get; set; } = false;

        /// <summary>
        ///线束
        /// </summary>
        public string WireHarness { get; set; }




        /// <summary>
        ///附件
        /// </summary>      
        public string Attachment { get; set; }


        /// <summary>
        ///订单状态
        /// </summary>    
        public OUT_ORDER_STATUS OrderStatus { get; set; } = OUT_ORDER_STATUS.Draft;



        /// <summary>
        ///是否草稿
        /// </summary>      
        public bool? DraftStatus { get; set; }

        public List<OutOrderDetailDto> OutOrderDetails { get; set; } = new List<OutOrderDetailDto>();

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