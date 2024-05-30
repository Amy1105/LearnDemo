using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    public class Order {
        [Key]
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsPay { get; set; } = false;

        public DateTime PayTime { get; set; }

        public int AddressID { get; set; }

        public Address address { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

    }


    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Order order { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
       

        public int Count { get; set; }
        public decimal Price { get; set; }

        public decimal Amount { get; set; }
        
    }

    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Postal_code { get; set; }

        public override string ToString()
        {
            return $"{Name}-{Province}-{City}-{District}-{Street}";
        }
    }
}
