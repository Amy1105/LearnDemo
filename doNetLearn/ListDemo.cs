using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn
{
    public class ListDemo
    {
        public void Compare()
        {
            object[] objects = { 1, 2, 3, 4, 5 };
            int[] ints = objects.Cast<int>().ToArray();

            object[] objects2 = { 1, 2, "three", 4, 5 };
            int[] ints2 = objects2.OfType<int>().ToArray();
        }
    }
}
