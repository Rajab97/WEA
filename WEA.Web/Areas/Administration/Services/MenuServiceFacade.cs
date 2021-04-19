using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;
using WEA.Web.Areas.Administration.Models;
using WEA.Web.Services;

namespace WEA.Web.Areas.Administration.Services
{
    public class MenuServiceFacade : BaseServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuServiceFacade(IUnitOfWork unitOfWork,
                                    IRepository<Menu> menuRepository,
                                        IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._menuRepository = menuRepository;
            this._mapper = mapper;
        }

        public Result Save(MenuViewModel model)
        {
            var dto = _mapper.Map<Menu>(model);
            return Succeed();
        }
    }
}
