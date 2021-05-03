using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Models
{
    public class RoleViewModel: BaseEntity
    {
        [Display(ResourceType = typeof(UI) , Name = nameof(UI.Name))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.IsSuperAdmin))]
        public bool IsSuperAdmin { get; set; }
    }
}
