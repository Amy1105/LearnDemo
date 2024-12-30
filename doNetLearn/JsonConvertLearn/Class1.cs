using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.JsonConvertLearn
{
    public class SubItem
    {
        public int SubId { get; set; }
        public string SubName { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubItem> SubItems { get; set; }
    }
}
