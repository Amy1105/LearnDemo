using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.Expressions
{
    public class MyClass
    {
        public void Print(string message)
        {
            Console.WriteLine($"Print: {message}");
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
