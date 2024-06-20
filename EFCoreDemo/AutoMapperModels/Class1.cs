using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo.AutoMapperModels
{
    public class Source
    {
        public int Value { get; set; }
    }

    public class Destination
    {
        public int Value { get; set; }
    }

    public class ParentSource
    {
        public int Value1 { get; set; }
    }

    public class ChildSource : ParentSource
    {
        public int Value2 { get; set; }
    }

    public class ParentDestination
    {
        public int Value1 { get; set; }
    }

    public class ChildDestination : ParentDestination
    {
        public int Value2 { get; set; }
    }

    public class SourceBuild
    {
        public int Value { get; set; }
    }
    public class SourceBuildDto
    {
        public SourceBuildDto(int valueParamSomeOtherName)
        {
            _value = valueParamSomeOtherName;
        }
        private int _value;
        public int Value
        {
            get { return _value; }
        }
    }

    public class Mapper_Order
    {
        private readonly IList<Mapper_OrderLineItem> _orderLineItems = new List<Mapper_OrderLineItem>();

        public Mapper_Customer Customer { get; set; }

        public Mapper_OrderLineItem[] GetOrderLineItems()
        {
            return _orderLineItems.ToArray();
        }

        public void AddOrderLineItem(Mapper_Product product, int quantity)
        {
            _orderLineItems.Add(new Mapper_OrderLineItem(product, quantity));
        }

        public decimal GetTotal()
        {
            return _orderLineItems.Sum(li => li.GetTotal());
        }
    }

    public class Mapper_Product
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
    }

    public class Mapper_OrderLineItem
    {
        public Mapper_OrderLineItem(Mapper_Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Mapper_Product Product { get; private set; }
        public int Quantity { get; private set; }

        public decimal GetTotal()
        {
            return Quantity * Product.Price;
        }
    }

    public class Mapper_Customer
    {
        public string Name { get; set; }
    }

}
