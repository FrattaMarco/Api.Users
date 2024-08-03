using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.CustomExceptions;
using Users.Application.Queries;
using Users.Application.Repositories;
using Users.Application.Responses;

namespace Users.Application.Handlers
{
    public class UserByIdHandler(IUsersRepository usersRepository, ILogger<UserByIdHandler> logger) : IRequestHandler<GetUserByIdQuery, UserResponseModel?>
    {
        private readonly IUsersRepository _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        private readonly ILogger<UserByIdHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<UserResponseModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Ricerca utente per i seguenti parametri: {request}", request);

            return await _usersRepository.GetUserById(request.IdUtente)
                ?? throw new UserNotFoundException($"Nessun utente trovato per l'id {request.IdUtente}");
        }
    }
}
