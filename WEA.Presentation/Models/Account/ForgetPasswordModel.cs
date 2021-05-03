using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Models
{
    public class ForgetPasswordModel
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.UserName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string UserName { get; set; }
    }
}
