using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace EFCoreDemo.Models.OutOrderHeaders
{
    public partial class OutOrderHeader
    {
        public ICollection<OutOrderDetail> OutOrderDetails { get; set; }

        public virtual ICollection<OutOrderSample> OutOrderSamples { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            if (string.IsNullOrEmpty(ApplicantDep))
            {
                yield return new ValidationResult("请选择申请部门", new[] { nameof(ApplicantDep) });
            }

            if (string.IsNullOrEmpty(OrderType))
            {
                yield return new ValidationResult("请选择项目类型", new[] { nameof(OrderType) });
            }

            if (string.IsNullOrEmpty(ProjectCode))
            {
                yield return new ValidationResult("请选择项目名称", new[] { nameof(ProjectCode) });
            }


            if (string.IsNullOrEmpty(TestClass))
            {
                yield return new ValidationResult("请选择测试类别", new[] { nameof(TestClass) });
            }

            if (string.IsNullOrEmpty(TaskPeriod))
            {
                yield return new ValidationResult("请选择测试周期", new[] { nameof(TaskPeriod) });
            }

            if (string.IsNullOrEmpty(TestPurposes))
            {
                yield return new ValidationResult("请填写测试目的", new[] { nameof(TestPurposes) });

                //测试目的数据库字数限制

                if (TestPurposes.Length > 1000)
                {
                    yield return new ValidationResult("测试目的超出数据最大限制长度", new[] { nameof(TestPurposes) });
                }
            }

            if (string.IsNullOrEmpty(NecessityAndValue))
            {
                yield return new ValidationResult("请填写必要性与价值", new[] { nameof(NecessityAndValue) });

                //测试目的数据库字数限制
                if (TestPurposes.Length > 1000)
                {
                    yield return new ValidationResult("必要性与价值超出数据最大限制长度", new[] { nameof(NecessityAndValue) });
                }
            }

            if (string.IsNullOrEmpty(SampleType))
            {
                yield return new ValidationResult("请选择样品大类", new[] { nameof(SampleType) });
            }

            if (string.IsNullOrEmpty(SampleSource))
            {
                yield return new ValidationResult("请选择样品来源", new[] { nameof(SampleSource) });
            }


            if (string.IsNullOrEmpty(SampleGroup))
            {
                yield return new ValidationResult("请选择样品类型", new[] { nameof(SampleGroup) });
            }

            if (string.IsNullOrEmpty(SampleSpec))
            {
                yield return new ValidationResult("请选择样品规格", new[] { nameof(SampleSpec) });
            }

            if (string.IsNullOrEmpty(SampleStage))
            {
                yield return new ValidationResult("请选择样品阶段", new[] { nameof(SampleStage) });
            }

            if (string.IsNullOrEmpty(SampleVersion))
            {
                yield return new ValidationResult("请选择样品版本号", new[] { nameof(SampleVersion) });
            }

            if (RequirementType <= 0)
            {
                yield return new ValidationResult("请选择需求类型", new[] { nameof(RequirementType) });
            }

            if (PlanningMethod <= 0)
            {
                yield return new ValidationResult("请选择检毕样品计划处理方式", new[] { nameof(PlanningMethod) });
            }

            if (string.IsNullOrEmpty(SampleSeries))
            {
                yield return new ValidationResult("请选择样品系列", new[] { nameof(SampleSeries) });
            }

            if (string.IsNullOrEmpty(SampleStatus))
            {
                yield return new ValidationResult("请选择样品状态", new[] { nameof(SampleStatus) });
            }

            if (IsPotentialRisk)
            {
                if (string.IsNullOrEmpty(PotentialRiskRemark))
                {
                    yield return new ValidationResult("请填写潜在异常风险描述", new[] { nameof(PotentialRiskRemark) });
                }
                //异常描述数据库字符限制
                if (PotentialRiskRemark.Length > 500)
                {
                    yield return new ValidationResult("潜在异常风险描述超出数据最大限制长度", new[] { nameof(NecessityAndValue) });
                }
            }


            if (string.IsNullOrEmpty(SampleSOC))
            {
                yield return new ValidationResult("请选择样品SOC", new[] { nameof(SampleSOC) });
            }

            if (IsFixture)
            {
                if (string.IsNullOrEmpty(Fixture))
                {
                    yield return new ValidationResult("请填写夹具", new[] { nameof(Fixture) });
                }
                //异常描述数据库字符限制
                if (Fixture.Length > 500)
                {
                    yield return new ValidationResult("夹具超出数据最大限制长度", new[] { nameof(Fixture) });
                }
            }

            if (IsWorkwear)
            {
                if (string.IsNullOrEmpty(Workwear))
                {
                    yield return new ValidationResult("请填写工装", new[] { nameof(Workwear) });
                }
                //异常描述数据库字符限制
                if (Workwear.Length > 500)
                {
                    yield return new ValidationResult("工装超出数据最大限制500长度", new[] { nameof(Workwear) });
                }
            }

            if (IsWireHarness)
            {
                if (string.IsNullOrEmpty(WireHarness))
                {
                    yield return new ValidationResult("请填写线束", new[] { nameof(WireHarness) });
                }
                //异常描述数据库字符限制
                if (WireHarness.Length > 500)
                {
                    yield return new ValidationResult("线束超出数据最大限制500长度", new[] { nameof(WireHarness) });
                }
            }
        }

    }

    public  enum OUT_ORDER_STATUS
    {
        None=-1,       
        [Description("草稿")]
        Draft = 0,           
        [Description("待提交")]
        WaitSubmit =1,        
        [Description("已提交")]
        Submitted =2,        
        [Description("审批中")]
        Ongoing =3,        
        [Description("已通过")]
        Pass =4,       
        [Description("已驳回")] 
        FailedPass =5,        
        [Description("在测")]
        Checking =6,       
        [Description("完成")]
        Finished =7,       
        [Description("修改申请单")]
        Edit =8,
    }
}
