using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.Core.Interfaces;
using WEA.Core.Repositories;

namespace WEA.Infrastructure.Data.Repositories
{
    public class AttachmentRepository : EfRepository<Attachment>,IAttachmentRepository
    {
        public AttachmentRepository(DbFactory dbFactory, ISessionService sessionService):base(dbFactory,sessionService)
        {

        }
    }
}
