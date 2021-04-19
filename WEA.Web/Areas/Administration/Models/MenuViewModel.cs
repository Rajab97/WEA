using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Web.Areas.Administration.Models
{
    public class MenuViewModel: BaseEntity
    {
        [Display(ResourceType = typeof(UI),Name = nameof(UI.Title))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages) , ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Title { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Name))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Icon))]
        public string Icon { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Css))]
        public string CssClass { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.URL))]
        public string Url { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Area))]
        public string Area { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Controller))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Controller { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Action))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string Action { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.BaseMenu))]
        public Guid? ParentId { get; set; }


        public List<SelectListItem> Menus { get; set; }
    }
}
