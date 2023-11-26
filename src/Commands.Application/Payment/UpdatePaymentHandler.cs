using Domain.Models;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand>
    {
        private readonly IPaymentService _paymentService;

        public UpdatePaymentHandler(IPaymentService paymentService) => _paymentService = paymentService;

        public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Id = request.Id,
                AccountId = request.AccountId,
                PaymentType = request.PaymentType,
                Sum = request.Sum,
                TimeStamp = request.TimeStamp,
            };

            await _paymentService.Update(payment);
        }
    }
}
