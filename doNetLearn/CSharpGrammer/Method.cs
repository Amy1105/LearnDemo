using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    public class CSharpGrammerMethod
    {
        public void Execute()
        {
            //
            ConvertsMethod m = new ConvertsMethod();
            m.ChangeTypeMethod();

            //类型转换
            TypeConversion typeConversion = new TypeConversion();
           typeConversion.Method();
           typeConversion.Method2();
           typeConversion.Method3();
            typeConversion.Method4();

            DatetimeDemo datetimeDemo = new DatetimeDemo();

            //多语言环境
            CultureInfoMethod cultureInfo = new CultureInfoMethod();


            CSharp11 cSharp11 = new CSharp11();

            CSharp13 cSharp13 = new CSharp13();
        }      
    }
}
