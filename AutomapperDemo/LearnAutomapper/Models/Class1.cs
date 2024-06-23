using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutomapper.Models
{
    public class ex_Order
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; }
        public string? OrderName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<ex_OrderDetail> Details { get; set; }
    }

    public class ex_OrderDetail
    {
        public int Id { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public int Count { get; set; }
    }


}
