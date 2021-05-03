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
using WEA.Core.Interfaces.Services;
namespace WEA.Web.Areas.Administration.Services
{
    public class MenuServiceFacade : BaseServiceFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuServiceFacade(IUnitOfWork unitOfWork,
                                    IMenuService menuService,
                                        IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._menuService = menuService;
            this._mapper = mapper;
        }

        public async Task<Result> SaveAsync(MenuViewModel model)
        {
            var dto = _mapper.Map<Menu>(model);
            var result = await _menuService.CreateAsync(dto);
            if (result.IsSucceed)
            {
                await _unitOfWork.CommitAsync();
                return Succeed();
            }
            return Failure(result.Exception);
        }

        public Result<IQueryable<Menu>> GetData()
        {
            return _menuService.GetAll();
        }
    }
}
