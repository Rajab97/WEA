using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WEA.SharedKernel
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        [NotMapped]
        public List<BaseDomainEvent> Events { get; set; } = new List<BaseDomainEvent>();
    }
}
