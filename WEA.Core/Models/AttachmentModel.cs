using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.Core.Models
{
    public class AttachmentModel
    {
        public Guid ProductId { get; set; }
        public IEnumerable<File> Files { get; set; }
        public string FolderPath { get; set; }


        public class File
        {
            public string FileName { get; set; }
            public string FileExtension { get; set; }
            public long ContentLength { get; set; }
            public string ContentType { get; set; }
            public string Description { get; set; }
            public byte[] Stream { get; set; }
        }
    }
}
