using System;
using System.Collections.Generic;
using System.Text;
using WEA.SharedKernel;

namespace WEA.Core.Entities
{
    public class Attachment : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
    }
}
