using MediatR;

namespace Users.Application.Commands
{
    public record DeleteUserCommand : IRequest
    {
        public int IdUtente { get; init; }
    }
}