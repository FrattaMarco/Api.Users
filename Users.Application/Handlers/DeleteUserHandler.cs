using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Commands;
using Users.Application.CustomExceptions;
using Users.Application.Repositories;

namespace Users.Application.Handlers
{
    public class DeleteUserHandler(IUsersRepository repository, ILogger<DeleteUserHandler> logger) : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUsersRepository _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly ILogger<DeleteUserHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Si procede alla rimozione dell'utente con IdUtente {IdUtente}", command.IdUtente);

            _ = await _userRepository.GetUserById(command.IdUtente)
            ?? throw new UserNotFoundException("Non è stato trovato l'utente per il quale si vuole procedere con la rimozione");

            await _userRepository.DeleteUser(command);
        }
    }
}