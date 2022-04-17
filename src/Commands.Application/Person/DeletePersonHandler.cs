using Domain.Models;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class DeletePersonHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IPersonService _personService;

        public DeletePersonHandler(IPersonService paymentService)
        {
            _personService = paymentService;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            await _personService.DeleteById(request.Id);
            
            return Unit.Value;
        }
    }
}
