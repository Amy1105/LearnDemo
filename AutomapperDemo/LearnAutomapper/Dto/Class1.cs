using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutomapper.Dto
{
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
