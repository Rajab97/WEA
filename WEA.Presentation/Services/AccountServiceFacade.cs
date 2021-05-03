using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Services
{
    public class AccountServiceFacade:BaseServiceFacade
    {
        private readonly UserManager<User> _userManager;

        public AccountServiceFacade(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Result<string> GenerateRandomPassword()
        {
            try
            {
                var options = _userManager.Options.Password;

                int length = options.RequiredLength;

                bool nonAlphanumeric = options.RequireNonAlphanumeric;
                bool digit = options.RequireDigit;
                bool lowercase = options.RequireLowercase;
                bool uppercase = options.RequireUppercase;

                StringBuilder password = new StringBuilder();
                Random random = new Random();

                while (password.Length < length)
                {
                    if (nonAlphanumeric)
                        password.Append((char)random.Next(33, 48));
                    if (digit)
                        password.Append((char)random.Next(48, 58));
                    if (lowercase)
                        password.Append((char)random.Next(97, 123));
                    if (uppercase)
                        password.Append((char)random.Next(65, 91));
                }

              

                return Succeed(password.ToString());
            }
            catch (ApplicationException ex)
            {
                ex.HandleException();
                return Result<string>.Failure(ex);
            }
            catch (Exception ex)
            {
                ex.HandleException();
                return Result<string>.Failure(ExceptionMessages.FatalError);
            }
        }
    }
}
