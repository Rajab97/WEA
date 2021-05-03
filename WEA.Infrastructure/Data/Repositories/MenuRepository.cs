using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEA.Core.Entities;
using WEA.Core.Repositories;
using WEA.Core.ViewEntities;

namespace WEA.Infrastructure.Data.Repositories
{
    public class MenuRepository : EfRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }

        public Menu GetMenuByRouteDetails(string area, string controller, string action)
        {
            if (String.IsNullOrEmpty(area))
                return _dbFactory.DbContext.Set<Menu>().Where(m => m.Controller == controller && m.Action == action).FirstOrDefault();
            else
                return _dbFactory.DbContext.Set<Menu>().Where(m => m.Area == area && m.Controller == controller && m.Action == action).FirstOrDefault();
        }

        public IQueryable<Menu> GetUserMenus(Guid id)
        {
            return _dbFactory.ViewsDbContext.UserMenusView.Where(m => m.UserId == id)
                        .Select(m => new Menu() {
                                Id = m.Id,
                                Action = m.Action,
                                Area = m.Area,
                                Controller = m.Controller,
                                CssClass = m.CssClass,
                                Icon = m.Icon,
                                IsVisible = m.IsVisible,
                                Name = m.Name,
                                Title = m.Title,
                                Url = m.Url,
                                Version = m.Version,
                                ParentId = m.ParentId
                            });
        }
    }
}
