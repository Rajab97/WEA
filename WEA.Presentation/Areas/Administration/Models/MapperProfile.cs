using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;

namespace WEA.Presentation.Areas.Administration.Models
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Menu, MenuViewModel>().ReverseMap();
            CreateMap<Role, RoleViewModel>().ReverseMap();
            CreateMap<RoleMenu, RoleMenuViewModel>().ReverseMap();
            CreateMap<Organization, OrganizationViewModel>().ReverseMap();
            CreateMap<UserViewModel, User>()
                .ForMember(d => d.Roles, s => s.MapFrom(x => x.Roles[0]))
                .ReverseMap()
                .ForMember(m=>m.Roles,s => s.MapFrom(x => !string.IsNullOrEmpty(x.Roles) ? x.Roles.Split(',', System.StringSplitOptions.None).ToArray() : new string[0]));
        }
    }
}
