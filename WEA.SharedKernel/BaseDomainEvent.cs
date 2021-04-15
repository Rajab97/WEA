using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEA.SharedKernel
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
