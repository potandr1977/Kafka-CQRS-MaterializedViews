using Domain.events;
using MediatR;

namespace Commands.Application.Notifications
{
    public record PaymentCreatedNotification: PaymentCreatedEvent, INotification
    {
    }
}
