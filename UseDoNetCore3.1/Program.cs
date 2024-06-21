using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System;
using System.Collections.Generic;
using UseDoNetCore3._1.Models;


namespace UseDoNetCore3._1
{
    internal class Program
    {    
        static void Main(string[] args)
        {
            IMapperConfigurationExpression cfg=new MapperConfigurationExpression();
            cfg.CreateMap<ThingDto, Thing>().EqualityComparison((dto, entity) => dto.ID == entity.ID);
            var map = new MapperConfiguration(cfg);
            map.CompileMappings();

            cfg.AddProfile<OrganizationProfile>();

            var dtos = new List<ThingDto>
            {
                new ThingDto { ID = 1, Title = "test0" },
                new ThingDto { ID = 2, Title = "test2" }
            };

            var items = new List<Thing>
            {
                new Thing { ID = 1, Title = "test1" },
                new Thing { ID = 3, Title = "test3" },
            };

            mapper.Map(dtos, items.ToList()).Should().HaveElementAt(0, items.First());
        }
    }
}
