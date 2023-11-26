using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application
{
    public class TaskWhenAllPublisher : INotificationPublisher
    {
        public Task Publish(
            IEnumerable<NotificationHandlerExecutor> handlerExecutors,
            INotification notification,
            CancellationToken cancellationToken)
        {
            var tasks = handlerExecutors
                .Select(handler => handler.HandlerCallback(
                    notification,
                    cancellationToken))
                .ToArray();

            return Task.WhenAll(tasks);
        }
    }
}
