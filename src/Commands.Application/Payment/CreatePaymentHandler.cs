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

        public CreatePaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
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

            await _paymentService.Save(payment);

            return newGuid;
        }
    }
}
