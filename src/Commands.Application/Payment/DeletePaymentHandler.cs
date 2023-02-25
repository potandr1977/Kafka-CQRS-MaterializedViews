using Domain.Models;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand>
    {
        private readonly IPaymentService _paymentService;

        public DeletePaymentHandler(IPaymentService paymentService) => _paymentService = paymentService;

        public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            await _paymentService.DeleteById(request.Id);

            return Unit.Value;
        }
    }
}
