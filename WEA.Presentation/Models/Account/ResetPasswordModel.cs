using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Models
{
    public class ResetPasswordModel
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.UserName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.NewPasswrod))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string NewPassword { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.ConfirmPassowrd))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
