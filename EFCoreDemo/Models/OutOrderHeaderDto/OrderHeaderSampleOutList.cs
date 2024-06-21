using System;
using VOL.Entity.DomainModels;

namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    public class OrderHeaderSampleOutList 
    {
        /// <summary>
        ///
        /// </summary>
        public string BarCode { get; set; }

        public string ScheduleStatus { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string OrderHerNo { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int taskProject { get; set; }


        /// <summary>
        /// 工单状态
        /// </summary>
        public string WorkOrderStatus { get; set; }
        /// <summary>
        /// 是否打印状态
        /// </summary>
        public bool isPageChannel { get; set; }

        public string TestPositionName;//测试厂区

        public string ActualPositionName;//实际厂区

        public string SampleType; //样品类型

        public string SampleSpecName;   //样品规格

        public bool? IsReceipted { get; set; }//是否收样

        public bool? IsSampleReturn { get; set; }//是否领回

        public bool? IsSampleScrap { get; set; }//是否报废
    }

}
