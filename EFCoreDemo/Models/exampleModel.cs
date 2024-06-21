using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    public class ex_Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }

        public string OrderName { get; set; }


        public bool IsDeleted { get; set; } = false;
        public List<ex_OrderDetail> Details { get; set; }
    }

    public class ex_OrderDetail
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Count { get; set; }
    }


    public class ex_OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public List<ex_OrderDetailDto> Details { get; set; }
    }

    public class ex_OrderDetailDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
    }
}
