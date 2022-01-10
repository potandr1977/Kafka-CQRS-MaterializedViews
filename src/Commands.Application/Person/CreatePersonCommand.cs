using MediatR;

namespace Commands.Application.Commands
{
    public class CreatePersonCommand : IRequest
    {
        public string Name { get; set; }

        public string Inn { get; set; }
    }
}
