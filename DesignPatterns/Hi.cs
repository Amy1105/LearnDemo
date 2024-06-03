using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class Hi
    {
        public string SayHi(string people)
        {
            return people switch
            {
                "A" => "早上好",
                "B" => "Good Moring",
                "C" => "xxx",
                _ => "lalala"
            };

        }
    }
}
