using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEA.Presentation.Helpers.Web
{
    public class SelectListItemGuid: SelectListItem
    {
        public SelectListItemGuid()
        {

        }
        public SelectListItemGuid(Guid id,string text)
        {
            Text = text;
            Id = id;
        }
        public SelectListItemGuid(Guid id, string text, bool isSelected) : this(id,text)
        {
            Selected = isSelected;
        }
        public Guid Id { get; set; }
    }
}
