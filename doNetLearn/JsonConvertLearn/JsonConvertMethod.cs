using doNetLearn.CSharpGrammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace doNetLearn.JsonConvertLearn
{
    public class JsonConvertMethod
    {
      public void Check()
        {

            //Json序列化如何实现的

            //为什么不一样呢？
            ParentClass parentClass = new ParentClass();

            ChildClass child = new ChildClass();
            string json = JsonSerializer.Serialize(child);
            Console.WriteLine(json);


            var s = parentClass.GetformValue(child);
            Console.WriteLine(s);


            DatetimeDemo.TimeZoneExample();

        }
    }

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
