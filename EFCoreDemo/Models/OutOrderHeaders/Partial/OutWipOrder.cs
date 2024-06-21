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
    /// 样品组任务（工单）
    /// </summary>
    public partial class OutWipOrder
    {      
        public virtual OutOrderDetail OutOrderDetail { get; set; }

        public virtual OutOrderHeader OutOrderHeader { get; set; }       
      

        public IEnumerable<ValidationResult> Validate()
        {

            //if (string.IsNullOrEmpty(OrderLineNo))
            //{
            //    yield return new ValidationResult("请选择申请部门", new[] { nameof(OrderLineNo) });
            //}

            ///// <summary>
            /////序号
            ///// </summary>
            //[Display(Name = "序号")]
            //[Column(TypeName = "int")]
            //public int SerialNo { get; set; }


            ///// <summary>
            /////顺序号
            ///// </summary>
            //[Display(Name = "顺序号")]
            //[Column(TypeName = "int")]
            //public int WipSequenceNo { get; set; }


            if (string.IsNullOrEmpty(BarCode))
            {
                yield return new ValidationResult("请填写样品生产条码", new[] { nameof(BarCode) });
            }

            if (Priority==0)
            {
                yield return new ValidationResult("请填写优先级", new[] { nameof(Priority) });
            }

            if (string.IsNullOrEmpty(ProductCode))
            {
                yield return new ValidationResult("请填写组别号", new[] { nameof(ProductCode) });
            }

            if (RatedCapacity<=0)
            {
                yield return new ValidationResult("请填写额定容量", new[] { nameof(RatedCapacity) });
            }

            if (RatedPower <= 0)
            {
                yield return new ValidationResult("请填写额定功率", new[] { nameof(RatedPower) });
            }

            if (Vmax <= 0)
            {
                yield return new ValidationResult("请填写Vmax", new[] { nameof(Vmax) });
            }
            if (Vmin <= 0)
            {
                yield return new ValidationResult("请填写Vmin", new[] { nameof(Vmin) });
            }
           
            if (Vmax < Vmin)
            {
                yield return new ValidationResult("样品生产条码：" + BarCode+"，Vmax必须大于Vmin", new[] { nameof(Vmax), nameof(Vmin) });
            }
        }       
    }

    public static class WIP_OUTORDER_STATUS
    {
        /// <summary>
        /// 初始状态：待测
        /// </summary>
        public static string WaitChecking = "待测";

        /// <summary>
        /// 第一个工序开始：在检
        /// </summary>
        public static string Checking = "在测";

        /// <summary>
        /// 全部工序完成：检毕
        /// </summary>
        public static string CheckFinish = "完成";

        /// <summary>
        /// 替换：终止
        /// </summary>
        public static string Ending = "终止";


        public static string Cancel = "取消测试";


        public static string OverdueScrap = "超期报废";


        public static string Scrap = "报废";
    }
}