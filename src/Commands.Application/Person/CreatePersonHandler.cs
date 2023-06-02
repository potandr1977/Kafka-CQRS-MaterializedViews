using Domain.Models;
using Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly IPersonService _personService;

        public CreatePersonHandler(IPersonService paymentService)
        {
            _personService = paymentService;
        }

        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var newGuid = Guid.NewGuid();
            var person = new Person
            {
                Id = newGuid,
                Name = request.Name,
                Inn = request.Inn,
            };

            await _personService.Create(person);

            return newGuid;
        }
    }
}
