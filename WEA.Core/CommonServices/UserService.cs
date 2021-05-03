using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.SharedKernel;
using WEA.SharedKernel.Exceptions;
using WEA.SharedKernel.Resources;

namespace WEA.Core.CommonServices
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager,
                            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Result<string> GetErrorMessageByErrorCode(string code)
        {

            try
            {
                return Result<String>.Succeed(ExceptionMessages.ResourceManager.GetString(code));
            }
            catch (BaseException exc)
            {
                return Result<string>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<string>.Failure(fatalExc);
            }
            
        }
    }
}
