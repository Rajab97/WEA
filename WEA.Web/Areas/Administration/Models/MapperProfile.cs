using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;

namespace WEA.Web.Areas.Administration.Models
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Menu, MenuViewModel>().ReverseMap();
        }
    }
}
