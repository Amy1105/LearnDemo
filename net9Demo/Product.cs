using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo
{
    internal class ProductClass
    {
        public int ProductClassID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }
     
        public int ProductClassID { get; set; }
    }


    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public enum Level { Beginner, Intermediate, Advanced }
}
