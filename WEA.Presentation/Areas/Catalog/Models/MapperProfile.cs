using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;

namespace WEA.Presentation.Areas.Catalog.Models
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CarBrandViewModel, CarBrand>().ReverseMap();
            CreateMap<CarModelViewModel, CarModel>().ReverseMap();
        }
    }
}
