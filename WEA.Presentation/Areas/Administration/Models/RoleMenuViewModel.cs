using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Models
{
    public class RoleMenuViewModel : BaseEntity
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.MenuName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public Guid? MenuID { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.RoleName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public Guid? RoleID { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.CanEdit))]
        public bool CanEdit { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.CanCreate))]
        public bool CanCreate { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.CanDelete))]
        public bool CanDelete { get; set; }
    }
}
