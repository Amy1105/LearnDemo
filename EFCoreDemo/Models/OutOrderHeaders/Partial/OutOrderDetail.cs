/*
 *代码由框架生成,任何更改都可能导致被代码生成器覆盖
 *如果数据库字段发生变化，请在代码生器重新生成此Model
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Models.OutOrderHeaders
{
    /// <summary>
    /// 测试任务
    /// </summary>
    public partial class OutOrderDetail
    {       
        public ICollection<OutWipOrder> OutWipOrders { get; set; }

        public OutOrderHeader OutOrderHeader { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            if (OrderLineNo==0)
            {
                yield return new ValidationResult("测试任务行号不能为空", new[] { nameof(OrderLineNo) });
            }
            
            if (TaskType == 0)
            {
                yield return new ValidationResult("请选择测试类型", new[] { nameof(TaskType) });
            }
            if (TaskProject == 0)
            {
                yield return new ValidationResult("请选择测试项目", new[] { nameof(TaskProject) });
            }
        }
    }
}