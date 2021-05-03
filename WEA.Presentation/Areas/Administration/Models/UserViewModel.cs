using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WEA.SharedKernel;
using WEA.SharedKernel.Resources;

namespace WEA.Presentation.Areas.Administration.Models
{
    public class UserViewModel : BaseEntity
    {
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.UserName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public virtual string UserName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Email))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Email))]
        public virtual string Email { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.PhoneNumber))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public virtual string PhoneNumber { get; set; }


        [Display(ResourceType = typeof(UI), Name = nameof(UI.WorkNumber))]
        public string WorkNumber { get; set; }


        [Display(ResourceType = typeof(UI), Name = nameof(UI.Name))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.LastName))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public string LastName { get; set; }


        [Display(ResourceType = typeof(UI), Name = nameof(UI.Patronymic))]
        public string Patronymic { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.DateOfBith))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public DateTime? DateOfBith { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Address))]
        public string Address { get; set; }

        [Display(ResourceType = typeof(UI), Name = nameof(UI.Roles))]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.Required))]
        public String[] Roles { get; set; }

    }
}
