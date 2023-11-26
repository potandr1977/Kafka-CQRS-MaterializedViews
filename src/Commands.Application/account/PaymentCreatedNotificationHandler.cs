using Commands.Application.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Accounts.Notifications
{
    public class PaymentCreatedNotificationHandler : INotificationHandler<PaymentCreatedNotification>
    {
        private readonly ILogger<PaymentCreatedNotificationHandler> _logger;

        public PaymentCreatedNotificationHandler(ILogger<PaymentCreatedNotificationHandler> logger) =>
            _logger = logger ?? throw new ArgumentException(nameof(logger));

        public async Task Handle(
            PaymentCreatedNotification notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account PaymentCreatedNotificatoin received");
        }
    }
}
