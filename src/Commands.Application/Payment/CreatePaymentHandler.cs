using Commands.Application.Notifications;
using Domain.Models;
using Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;

        public CreatePaymentHandler(IPaymentService paymentService, IMediator mediator)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var newGuid = Guid.NewGuid();

            var payment = new Payment
            {
                Id = newGuid,
                AccountId = request.AccountId,
                PaymentType = request.PaymentType,
                Sum = request.Sum
            };

            await _paymentService.Create(payment);


            await _mediator.Publish(new PaymentCreatedNotification 
            { 
                Id = newGuid,
                AccountId = request.AccountId,
                Sum = request.Sum
            });
           

            return newGuid;
        }
    }
}
