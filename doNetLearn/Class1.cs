using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace doNetLearn
{
    public class ParentClass
    {
        public int ParentProperty { get; set; } = 1;

        public string GetformValue(ParentClass oaParam)
        {
            var s = JsonSerializer.Serialize(oaParam);
            return s;
        }

    }

    public class ChildClass : ParentClass
    {
        public int ChildProperty { get; set; } = 2;
    }
}
