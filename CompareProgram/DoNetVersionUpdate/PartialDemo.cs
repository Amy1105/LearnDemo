using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetVersionUpdate
{
    internal class PartialDemo
    {
    }

    interface IStudent
    {
        string GetName();
    }

    partial class C : IStudent
    {
        public virtual partial string GetName();
    }

    partial class C
    {
        public virtual partial string GetName() => "Jarde";
    }


    partial class D
    {
        // Okay because no definition is required here
        partial void M1();

        // Okay because M2 has a definition
        public  partial void M2();

        // Error: partial method M3 must have a definition
        public partial void M3();
    }

    partial class D
    {
        public partial void M2() 
        {
            Console.WriteLine("m2");
        }

        public partial void M3()
        {
            Console.WriteLine("m3");
        }
    }
}
