using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Catalog.Models
{
    public class CarBrandViewModel : BaseEntity
    {
        [Display(ResourceType = typeof(UI), Name = nameof(UI.Name))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Code))]
        public string Code { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Description))]
        public string Description { get; set; }
    }
}
