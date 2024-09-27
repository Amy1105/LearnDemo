using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DataStructures
{
    internal class ReadOnlyCollectionCls
    {

        //ReadOnlyCollection<T>是IList<T>接口得一个只读实现，确保集合得数据不会被外部修改。

        //无论是ReadOnlyCollection还是 ，都只是提供了一个只读得视图，而不是创建了数据的副本，这意味着，如果原始list被修改，只读集合中的数据也会相应地发生变化。

        public void Fun()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ReadOnlyCollection<int> list2 = new(list);
            ReadOnlyCollection<int> list3 = list.AsReadOnly();
            int firstElement = list[0];
            list[0] = 1;

            foreach (int element in list)
            {
                Console.WriteLine(element);
            }

            foreach (int element in list2)
            {
                Console.WriteLine(element);
            }

            foreach (int element in list3)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("Hello, World!");
        }
    }
}