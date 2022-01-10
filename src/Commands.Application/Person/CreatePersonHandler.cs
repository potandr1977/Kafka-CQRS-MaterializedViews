using Domain.Models;
using Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Application.Commands
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand>
    {
        private readonly IPersonService _personService;

        public CreatePersonHandler(IPersonService accountService)
        {
            _personService = accountService;
        }

        public async Task<Unit> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Inn = request.Inn
            };

            await _personService.Save(person);

            return Unit.Value;
        }
    }
}
