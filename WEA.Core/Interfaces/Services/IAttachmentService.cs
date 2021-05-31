using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEA.Core.Entities;
using WEA.Core.Models;
using WEA.SharedKernel;
using WEA.SharedKernel.Interfaces;

namespace WEA.Core.Interfaces.Services
{
    public interface IAttachmentService : IBaseService<Attachment>
    {
        public Task<Result> UploadAsync(AttachmentModel attachments);
        public Task<Result> RemoveByProductIdAsync(Guid Id);
    }
}
