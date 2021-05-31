using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Models
{
    public class OrganizationViewModel : BaseEntity
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.OrganizationName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string OrganizationName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.OrganizationAddress))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string OrganizationAddress { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.PhoneNumber))]
        public string TelephoneNumber { get; set; }

    }
}
