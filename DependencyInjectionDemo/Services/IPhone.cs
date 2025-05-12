using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
   internal interface  IPhone
    {
        string GetName();

        string GetDescription();
    }

    internal class OppOPhone : IPhone
    {
        public string GetDescription()
        {
            return GetType().Name+ "Description";
        }

        public string GetName()
        {
            return GetType().Name;
        }
    }

    internal class HuaweiPhone : IPhone
    {
        public string GetDescription()
        {
            return GetType().Name + "Description";
        }

        public string GetName()
        {
            return GetType().Name;
        }
    }

    internal class HonorPhone : IPhone
    {
        public string GetDescription()
        {
            return GetType().Name + "Description";
        }

        public string GetName()
        {
            return GetType().Name;
        }
    }

    internal class XiaoMiPhone : IPhone
    {
        public string GetDescription()
        {
            return GetType().Name + "Description";
        }

        public string GetName()
        {
            return GetType().Name;
        }
    }
}
