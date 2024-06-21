using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VOL.Entity.DomainModels;

namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    public class OutWipOrderDto
    {        
        public int? OutWipOrderID { get; set; }


        /// <summary>
        ///
        /// </summary>       
        public int? OrderLineNo { get; set; }

        /// <summary>
        ///样品编号
        /// </summary>     
        public string TestNo { get; set; }

        /// <summary>
        ///电芯号
        /// </summary>      
        public string BarCode { get; set; }


        /// <summary>
        ///工单状态
        /// </summary>      
        public string STATUS { get; set; } 


        /// <summary>
        ///样品组总数量
        /// </summary>      
        public decimal OrderQuantity { get; set; }

       
        public int OutOrderSampleId { get; set; }


        /// <summary>
        ///优先级
        /// </summary>     
        public int Priority { get; set; }


        /// <summary>
        ///序号
        /// </summary>
       
        public int SerialNo { get; set; }


        /// <summary>
        ///顺序号
        /// </summary>     
        public int WipSequenceNo { get; set; }

        /// <summary>
        ///
        /// </summary>      
        public int? OutOrderHeaderID { get; set; }

        /// <summary>
        ///
        /// </summary>      
        public int? OutOrderDetailId { get; set; }

        /// <summary>
        ///产品主键
        /// </summary>      
        public int ProductID { get; set; }


        /// <summary>
        ///
        /// </summary>       
        public string ProductCode { get; set; }


        /// <summary>
        ///Vmax
        /// </summary>      
        public decimal Vmax { get; set; }

        /// <summary>
        ///Vmin
        /// </summary>      
        public decimal Vmin { get; set; }

        /// <summary>
        ///额定容量
        /// </summary>       
        public decimal RatedCapacity { get; set; }

        /// <summary>
        /// 额定功率
        /// </summary>
        public decimal RatedPower { get; set; }       
    }
}
