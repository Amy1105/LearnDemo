using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VOL.Entity.DomainModels;

namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    public class ChangeOutOrderHeaderAuditRemarkParam
    {
        [Display(Name = "申请单Id")]
        public int? orderHeaderId { get; set; }


        [Display(Name = "申请单审核备注")]
        public string auditRemark { get; set; }


        [Display(Name = "备注")]
        public string remark { get; set; }

    }

    public class MergeIndex
    {
        public int beginIndex { get; set; }

        public int endIndex { get; set; }
    }



    /// <summary>
    /// 提交审核请求更新信息
    /// </summary>
    public class OutOrderHeaderParam
    {
        public string updateFlg { get; set; }
        public int ID { get; set; }
        public string auditRemark { get; set; }

        public bool IsAuditBySelf { get; set; }
    }

    public class OutOrderHeaderStatus
    {
        [Display(Name = "申请单Id")]
        public int orderHeaderId { get; set; }


        [Display(Name = "申请单状态")]
        public string orderStatus { get; set; }


        [Display(Name = "备注")]
        public string remark { get; set; }
    }

    public class OutOrderHeaderNoParam
    {
        [Display(Name = "申请单Id")]
        public int? orderHeaderId { get; set; }


        [Display(Name = "申请单号")]
        public string newOrderHerNo { get; set; }


        [Display(Name = "备注")]
        public string remark { get; set; }
    }

    /// <summary>
    /// 安全测试费用汇总
    /// </summary>
    public class CostAggregationModel
    {
        /// <summary>
        /// 项目类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 样品类型
        /// </summary>
        public int SampleType { get; set; }

        /// <summary>
        /// 测试项目
        /// </summary>
        public int TaskProject { get; set; }


        /// <summary>
        /// 测试项目
        /// </summary>
        public string TaskProjectName { get; set; }

        /// <summary>
        /// 预估费用
        /// </summary>
        public int EstimatedCost { get; set; }

        public int OrderHeaderID { get; set; }

        public int OrderDetailId { get; set; }

        /// <summary>
        /// 样品生产条码
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string OrderHerNo { get; set; }

        /// <summary>
        /// 样品生产条码数量
        /// </summary>
        public int BarCodeNumber { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// 申请单数量
        /// </summary>
        public int RequisitionQty { get; set; }

        /// <summary>
        /// 测试依据
        /// </summary>
        public string ProcessClassName { get; set; }

    }

    public class OutOrderHeaderT
    {
        public int OrderHeaderID { get; set; }

        /// <summary>
        ///申请编号
        /// </summary>
        public string OrderHerNo { get; set; }

        /// <summary>
        ///申请日期
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        ///项目类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        ///WEB编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        ///申请人
        /// </summary>
        public string Applicant { get; set; }

        /// <summary>
        ///申请部门
        /// </summary>
        public string ApplicantDep { get; set; }

        /// <summary>
        ///是否加急
        /// </summary>
        public bool? IsUrgent { get; set; }

        /// <summary>
        ///项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        ///订单状态
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        ///审核信息
        /// </summary>
        public string AuditRemark { get; set; }

        /// <summary>
        ///测试目的
        /// </summary>
        public string TestPurposes { get; set; }

        /// <summary>
        ///样品来源
        /// </summary>
        public string SampleSource { get; set; }

        /// <summary>
        ///样品阶段
        /// </summary>
        public string SampleStage { get; set; }

        /// <summary>
        ///样品版本号
        /// </summary>
        public string SampleVersion { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
        public string SampleSpec { get; set; }

        /// <summary>
        ///数量
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        ///数量
        /// </summary>
        public int? SampleQuantity { get; set; }


        /// <summary>
        ///在检
        /// </summary>
        public int? UnderInspection { get; set; }


        /// <summary>
        ///检闭
        /// </summary>
        public int? InspectionCompleted { get; set; }

        /// <summary>
        ///样品类别
        /// </summary>
        public string SampleType { get; set; }

        /// <summary>
        ///测试类别
        /// </summary>
        public string TestClass { get; set; }

        /// <summary>
        ///备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///测试项目
        /// </summary>
        public string TaskProject { get; set; }

        /// <summary>
        ///测试周期
        /// </summary>
        public string TaskPeriod { get; set; }

        /// <summary>
        ///BOP类别
        /// </summary>
        public int? ProcessType { get; set; }

        /// <summary>
        ///测试依据
        /// </summary>
        public int? ProcessClass { get; set; }

        /// <summary>
        ///预估费用
        /// </summary>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        ///实际费用
        /// </summary>
        public decimal? ActualCost { get; set; }

        /// <summary>
        ///测试状态
        /// </summary>
        public string TestStatus { get; set; }

        /// <summary>
        ///申请人主键
        /// </summary>
        public int? ApplicantId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int? PackageId { get; set; }

        /// <summary>
        ///样品描述
        /// </summary>
        public string SampleDescribe { get; set; }

        /// <summary>
        ///产品PN
        /// </summary>
        public string ProductPN { get; set; }

        /// <summary>
        ///样品类型
        /// </summary>
        public string SampleTypeName { get; set; }

        /// <summary>
        ///样品规格
        /// </summary>
        public string SampleSpecName { get; set; }

        /// <summary>
        ///修改原因
        /// </summary>
        public string EditReason { get; set; }

        /// <summary>
        ///已收样样品数量
        /// </summary>
        public int? ReceivedQuantity { get; set; }

        /// <summary>
        ///是否高危
        /// </summary>
        public bool? IsRisk { get; set; }

        /// <summary>
        ///是否草稿
        /// </summary>
        public bool? DraftStatus { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "创建人")]
        public string Creator { get; set; }

        [Display(Name = "修改时间")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "修改人")]
        public string Modifier { get; set; }
    }


    public class StatisticsOutList
    {
        public string BarCode { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string OrderHerNo { get; set; }
        public bool isPageChannel { get; set; }
    }
    public class Childrens
    {
        public string OrderHerNo { get; set; }
        public int OrderHeaderId { get; set; }

        public string BarCode { get; set; }
        public string Applicant { get; set; }
        public string TestNo { get; set; }
        //public int SampleQuantity { get; set; } //样品总数量
        //public int ReceivedQuantity { get; set; }//已收样数量
        public int SumTask { get; set; } //总任务
        public int AccomplishTask { get; set; }//完成任务数
        public int TestTask { get; set; } //待测任务数
        public int InTestTask { get; set; } //在测任务数
        public int SumProcess { get; set; }//总工序
        public int InTestProcess { get; set; }//在测工序数
        public int AccomplishProcess { get; set; }//完成工序数
        public string ProcessPlan { get; set; } //工序完成进度

    }
    public class Star
    {
        public string OrderHerNo { get; set; }
        public int OrderHeaderId { get; set; }

        public string BarCode { get; set; }
        public string Applicant { get; set; }
        public string TestNo { get; set; }
        //public int SampleQuantity { get; set; } //样品总数量
        //public int ReceivedQuantity { get; set; }//已收样数量
        public int SumTask { get; set; } //总任务
        public int AccomplishTask { get; set; }//完成任务数
        public int TestTask { get; set; } //待测任务数
        public int InTestTask { get; set; } //在测任务数
        public int SumProcess { get; set; }//总工序
        public int InTestProcess { get; set; }//在测工序数
        public int AccomplishProcess { get; set; }//完成工序数
        public string ProcessPlan { get; set; } //工序完成进度

    }

    public class Statistics
    {
        public Statistics()
        {
            this.children = new List<Childrens>();
        }

        public string OrderHerNo { get; set; }
        public int OrderHeaderId { get; set; }
        public List<Childrens> children;
        public string BarCode { get; set; }
        public string Applicant { get; set; }
        public string TestNo { get; set; }
        //public int SampleQuantity { get; set; } //样品总数量
        //public int ReceivedQuantity { get; set; }//已收样数量
        public int SumTask { get; set; } //总任务
        public int AccomplishTask { get; set; }//完成任务数
        public int TestTask { get; set; } //待测任务数
        public int InTestTask { get; set; } //在测任务数
        public int SumProcess { get; set; }//总工序
        public int InTestProcess { get; set; }//在测工序数
        public int AccomplishProcess { get; set; }//完成工序数
        public string ProcessPlan { get; set; } //工序完成进度


    }
}
