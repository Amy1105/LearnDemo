using System;
using System.Collections.Generic;
using System.Text;

namespace UseDoNetCore3._1.Models
{
    public class ThingDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }

    public class Thing
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public override string ToString() { return Title; }
    }
}
