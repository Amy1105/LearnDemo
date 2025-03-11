using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    public class Method
    {
        public void Execaute()
        {
            //
            ConvertsMethod m = new ConvertsMethod();
            m.ChangeTypeMethod();

            //类型转换
            TypeConversion typeConversion = new TypeConversion();

            DatetimeDemo datetimeDemo = new DatetimeDemo();

            //多语言环境
            CultureInfoMethod cultureInfo = new CultureInfoMethod();


            CSharp11 cSharp11 = new CSharp11();

            CSharp13 cSharp13 = new CSharp13();
        }      
    }
}
