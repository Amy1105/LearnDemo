using EFCoreDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Services
{
    public class EnumService
    {
        private readonly SchoolContext context;
        public EnumService(SchoolContext _context)
        {
            context = _context;
        }


        public void InsertAddress()
        {
            Address address = new Address()
            {
                Name = "Amy",
                Phone = "15997647389",
                Province = "江苏省",
                City = "南京市",
                District = "雨花区",
                Street = "郁金香路16号",
                Postal_code = "210000",
                status = Status.Success
            };
            context.Addresss.Add(address);
            context.SaveChanges();
        }

        public void Search()
        {
            var query = context.Addresss.Where(x => x.status == Status.Success);
            var list=  query.ToList();
        }

    }
}
