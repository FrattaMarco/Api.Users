using MediatR;

namespace Users.Application.Commands
{
    public record CreateUserCommand : IRequest
    {
        public string Email { get; init; } = null!;
        public string Nome { get; init; } = null!;
        public string Cognome { get; init; } = null!;
        public string CodiceFiscale { get; init; } = null!;
        public string Indirizzo { get; init; } = null!;
        public DateTime DataNascita { get; init; }
        public string Password { get; init; } = null!;
    }
}
