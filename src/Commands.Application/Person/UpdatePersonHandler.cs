using Domain.Models;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand>
    {
        private readonly IPersonService _personService;

        public UpdatePersonHandler(IPersonService paymentService)
        {
            _personService = paymentService;
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                Id = request.Id,
                Name = request.Name,
                Inn = request.Inn
            };

            await _personService.Save(person);

            return Unit.Value;
        }
    }
}
