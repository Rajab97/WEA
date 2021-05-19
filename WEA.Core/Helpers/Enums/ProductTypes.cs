using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WEA.SharedKernel.Resources;

namespace WEA.Core.Helpers.Enums
{
    public enum ProductType
    {
        [Display(ResourceType = typeof(UI),Name = nameof(UI.CarPart))]
        CarParts = 1
    }
}
