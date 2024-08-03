using MediatR;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public record UpdateUserCommand : IRequest<UserResponseModel>
    {
        public int IdUtente { get; init; }
        public UpdateUserBody Body { get; init; } = null!;
    }

    public class UpdateUserBody
    {
        public string? Email { get; init; }
        public string? Nome { get; init; } = null!;
        public string? Cognome { get; init; } = null!;
        public string? CodiceFiscale { get; init; } = null!;
        public string? Indirizzo { get; init; } = null!;
        public DateTime? DataNascita { get; init; }
        public string? Password { get; init; } = null!;
    }
}
