using System;
using System.Collections.Generic;
using System.Text;
using WEA.Core.Entities;
using WEA.SharedKernel;

namespace WEA.Core.Events
{
    public class TestEntityCompletedEvent : BaseDomainEvent
    {
        public string NotificationMessage { get; set; }
        public string ForWhom { get; set; }
        public string Title { get; set; }

        public TestEntityCompletedEvent(string nm,string fw,string title)
        {
            NotificationMessage = nm;
            ForWhom = fw;
            Title = title;
        }
    }
}
