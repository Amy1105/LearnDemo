using AutoMapper;
using LearnAutomapper.Dto;
using LearnAutomapper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnAutomapper
{
    public class ex_orderService
    {
        private readonly IMapper mapper;
        private readonly SchoolContext context;

        public ex_orderService(IMapper mapper, SchoolContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public void Add()
        {
            var orderDto = new ex_OrderDto
            {
                OrderNumber = "20210801",                
                Details = new List<ex_OrderDetailDto>
                {
                    new ex_OrderDetailDto {  ItemName = "Item1" },
                    new ex_OrderDetailDto {  ItemName = "Item2" }
                }
            };
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ex_OrderDetailDto, ex_OrderDetail>();   //往 Profile 类中的List<TypeMapConfiguration> _typeMapConfigs 对象中添加
                cfg.CreateMap<ex_OrderDto, ex_Order>();
            });
            var mapper = config.CreateMapper();
            var order = mapper.Map<ex_Order>(orderDto);

            context.Add(order);
            context.SaveChanges();
        }

        public void SelectCourse()
        {
            var courses = context.ex_Orders.Include(x => x.Details).ToList();
            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    Print(course);
                }
            }
        }

        private void Print(ex_Order order)
        {
            if (order != null)
            {
                Console.WriteLine($"order:{order.Id},{order.OrderName}");
                if (order.Details.Any())
                {
                    foreach (var detail in order.Details)
                    {
                        Console.WriteLine($"----detail :{detail.Id}-{detail.ItemName}");                        
                    }
                }
            }
        }


    }
}
