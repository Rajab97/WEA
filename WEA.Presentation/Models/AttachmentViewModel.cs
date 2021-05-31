using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEA.Presentation.Models
{
    public class AttachmentViewModel
    {
        public Guid ProductId { get; set; }
        public List<string> Base64String { get; set; }
        public List<string> FileName { get; set; }
        public List<string> ContentType { get; set; }
    }
}
