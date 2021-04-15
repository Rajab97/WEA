using Ardalis.GuardClauses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WEA.Core.Events;

namespace WEA.Core.Handlers
{
    public class TestEntityCompletedEventHandler : INotificationHandler<TestEntityCompletedEvent>
    {

        public TestEntityCompletedEventHandler()
        {
        }

        // configure a test email server to demo this works
        // https://ardalis.com/configuring-a-local-test-email-server
        public Task Handle(TestEntityCompletedEvent domainEvent, CancellationToken cancellationToken)
        {
            Guard.Against.Null(domainEvent, nameof(domainEvent));
            return Task.CompletedTask;
        }
    }
}
