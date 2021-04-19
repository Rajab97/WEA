using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class Menu : BaseEntity
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public string Url { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Guid? ParentId { get; set; }
        public Menu Parent { get; set; }
    }
}
