using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UseDoNetCore3._1.Models;
using VOL.Core.Extensions.AutofacManager.EqualityComparisonFun;

namespace UseDoNetCore3._1
{
    public class OrganizationProfile:Profile
    {
        public OrganizationProfile()
        {
            CreateMap<ThingDto, Thing>().EqualityComparison((dto, entity) => dto.ID == entity.ID);
        }
    }
}
