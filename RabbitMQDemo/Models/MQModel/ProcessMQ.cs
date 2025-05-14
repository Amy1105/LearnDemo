using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VOL.Entity.DomainModels;

namespace VOL.Entity.MQModel
{
    public  class ProcessMQ
    {
        public Process GetByProcessMQ(int processTypeId,int processClassId)
        {
            Process process = new Process();
            //ProcessId = processId;
            process.ProcessTypeId = processTypeId;
            process.ProcessStatus =this.ProcessStatus;
            process.ProcessClassId = processClassId;
            process.IsTemplate =this.IsTemplate;
            process.ProjectID =this.ProjectID;
            process.OwnerFacility =this.OwnerFacility;
            process.Company =this.Company;
            process.DiscontinueDate =this.DiscontinueDate;
            process.EffectiveDate =this.EffectiveDate;
            process.WBSCode =this.WBSCode;
            process.RevisionStatusID =this.RevisionStatusID;
            process.DefaultProcessRevision =this.DefaultProcessRevision;
            //ParentProcessID = processMQ.ParentProcessID;
            process.Remark =this.Remark;
            process.TestContent =this.TestContent;
            process.TestClass =this.TestClass;           
            process.TestType =this.TestType;
            process.TestStandard =this.TestStandard;
            process.ProcessDesc =this.ProcessDesc;
            process.ProcessName =this.ProcessName;
            process.ProcessRevision =this.ProcessRevision;
            process.Enable =this.Enable;
            process.IsTemp =this.IsTemp;
            process.RuleAsk =this.RuleAsk;
            process.FixtureAsk =this.FixtureAsk;
            process.TempAsk =this.TempAsk;
            process.GetPintAsk =this.GetPintAsk;
            process.ProtectionConditions =this.ProtectionConditions;
            process.OtherAsk =this.OtherAsk;
            process.TestReportAsk =this.TestReportAsk;
            process.PickPint =this.PickPint;
            process.BopWorkTime =this.BopWorkTime;
            process.SampleGroup =this.SampleGroup;
            process.OldProcessID =this.OldProcessID;
            process.BopStatus =this.BopStatus;
            process.Version =this.Version;
            process.FlowType =this.FlowType;
            process.ExamineUserID =this.ExamineUserID;
            process.SubmitUserID =this.SubmitUserID;
            process.ExamineUserOneID =this.ExamineUserOneID;
            process.ExamineUserTwoID =this.ExamineUserTwoID;
            process.TestPassStandard =this.TestPassStandard;
            process.ProjectType =this.ProjectType;
            //process.ProjectTypeCode = processMQ.ProjectTypeCode;
            process.ProjectAttribute =this.ProjectAttribute;
            process.TestItem =this.TestItem;
            //process.TestClassEnum = processMQ.TestClassEnum;
            process.OtherProcessName =this.OtherProcessName;
            //process.SampleGroupCode = processMQ.SampleGroupCode;          
            return process;
        }

        /// <summary>
        ///测试流程主键
        /// </summary>     
        public int ProcessId { get; set; }

        /// <summary>
        ///流程类型主键
        /// </summary>       
        public int? ProcessTypeId { get; set; }

        /// <summary>
        ///流程状态
        /// </summary>       
        public Process_State? ProcessStatus { get; set; }

        /// <summary>
        ///流程类别主键
        /// </summary>      
        public int? ProcessClassId { get; set; }

        /// <summary>
        ///是模板
        /// </summary>      
        public bool? IsTemplate { get; set; }

        /// <summary>
        ///项目主键
        /// </summary>       
        public int? ProjectID { get; set; }

        /// <summary>
        ///属于工厂
        /// </summary>       
        public string OwnerFacility { get; set; }

        /// <summary>
        ///公司名称
        /// </summary>      
        public string Company { get; set; }

        /// <summary>
        ///停止日期
        /// </summary>      
        public DateTime? DiscontinueDate { get; set; }

        /// <summary>
        ///生效日期
        /// </summary>      
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        ///WBS编码
        /// </summary>      
        public string WBSCode { get; set; }

        /// <summary>
        ///版本状态主键
        /// </summary>     
        public int? RevisionStatusID { get; set; }

        /// <summary>
        ///默认流程版本
        /// </summary>      
        public bool? DefaultProcessRevision { get; set; }

        /// <summary>
        ///上级流程主键
        /// </summary>     
        public int? ParentProcessID { get; set; }

        /// <summary>
        ///备注
        /// </summary>     
        public string Remark { get; set; }

        /// <summary>
        ///测试内容
        /// </summary>     
        public string TestContent { get; set; }

        /// <summary>
        ///测试类别
        /// </summary>      
        public string TestClass { get; set; }

        /// <summary>
        ///测试项目
        /// </summary>       
        public string TestItem { get; set; }

        /// <summary>
        ///测试类型
        /// </summary>      
        public string TestType { get; set; }

        /// <summary>
        ///测试依据
        /// </summary>      
        public string TestStandard { get; set; }

        /// <summary>
        ///流程描述
        /// </summary>      
        public string ProcessDesc { get; set; }

        /// <summary>
        ///流程名称
        /// </summary>      
        public string ProcessName { get; set; }

        /// <summary>
        ///流程版本
        /// </summary>      
        public string ProcessRevision { get; set; }

        /// <summary>
        ///启用
        /// </summary>    
        public bool? Enable { get; set; }

        /// <summary>
        ///特殊温度
        /// </summary>     
        public bool? IsTemp { get; set; }

        /// <summary>
        ///常规要求
        /// </summary>     
        public string RuleAsk { get; set; }

        /// <summary>
        ///夹具要求
        /// </summary>      
        public string FixtureAsk { get; set; }

        /// <summary>
        ///温控要求
        /// </summary>      
        public string TempAsk { get; set; }

        /// <summary>
        ///取点要求
        /// </summary>      
        public string GetPintAsk { get; set; }

        /// <summary>
        ///保护条件
        /// </summary>      
        public string ProtectionConditions { get; set; }

        /// <summary>
        ///其他要求
        /// </summary>       
        public string OtherAsk { get; set; }

        /// <summary>
        ///测试报告
        /// </summary>      
        public string TestReportAsk { get; set; }

        /// <summary>
        ///采点要求
        /// </summary>      
        public string PickPint { get; set; }


        /// <summary>
        /// 工时
        /// </summary>
        public int? BopWorkTime { get; set; }

        /// <summary>
        /// 样品类型
        /// </summary>      
        public string SampleGroup { get; set; }

        /// <summary>
        /// 老BopID
        /// </summary>       
        public int? OldProcessID { get; set; }

        /// <summary>
        /// BOP审批状态
        /// </summary>      
        public int? BopStatus { get; set; }

        /// <summary>
        /// Bop版本
        /// </summary>      
        public int? Version { get; set; }

        /// <summary>
        /// 审批流级别
        /// </summary>      
        public string FlowType { get; set; }

        /// <summary>
        /// 当前审批人
        /// </summary>       
        public int? ExamineUserID { get; set; }

        /// <summary>
        /// 当前提交人
        /// </summary>     
        public int? SubmitUserID { get; set; }

        /// <summary>
        /// 一级审批人
        /// </summary>      
        public int? ExamineUserOneID { get; set; }

        /// <summary>
        /// 二级审批人
        /// </summary>     
        public int? ExamineUserTwoID { get; set; }

        /// <summary>
        /// 测试通过标准
        /// </summary>      
        public string TestPassStandard { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>      
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目类型枚举值
        /// </summary>      
        public string ProjectTypeCode { get; set; }

        /// <summary>
        /// 项目属性
        /// </summary>       
        public string ProjectAttribute { get; set; }

        /// <summary>
        /// 测试类别
        /// </summary>      
        public string TestClassEnum { get; set; }

       /// <summary>
       /// 
       /// </summary>
        public string OtherProcessName { get; set; }


        /// <summary>
        /// 样品类型
        /// </summary>
        public string SampleGroupCode { get; set; }

        /// <summary>
        /// bop库类型
        /// </summary>
        public string ProcessClass { get; set; }

        /// <summary>
        /// bop库名称
        /// </summary>
        public string ProcessType { get; set; }

        [ForeignKey("ProcessId")]
        public List<ProcessOperationMQ> processOperationMQs { get; set; }
    }
}
