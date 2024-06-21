using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreDemo.OutOrderHeaderModule.Dto
{
    public class OutOrderDetailDto
    {
        /// <summary>
        ///测试任务主键
        /// </summary>      
        public int? OutOrderDetailID { get; set; }

        /// <summary>
        ///
        /// </summary>    
        public int? OutOrderHeaderID { get; set; }

        /// <summary>
        ///测试任务行号
        /// </summary>     
        public int OrderLineNo { get; set; } = 0;

        /// <summary>
        ///测试类型
        /// </summary>      
        public int TaskType { get; set; }


        /// <summary>
        ///测试类型value
        /// </summary>     
        public string TaskTypeName { get; set; }

        /// <summary>
        ///测试项目
        /// </summary>     
        public int TaskProject { get; set; }

        /// <summary>
        ///测试项目Value
        /// </summary>     
        public string TaskProjectName { get; set; }

        public List<OutWipOrderDto> OutWipOrderDtos { get; set; } = new List<OutWipOrderDto>();

    }
}
