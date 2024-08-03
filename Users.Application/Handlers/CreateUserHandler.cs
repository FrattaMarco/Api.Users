using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Commands;
using Users.Application.CustomExceptions;
using Users.Application.Repositories;

namespace Users.Application.Handlers
{
    public class CreateUserHandler(IUsersRepository repository, ILogger<CreateUserHandler> logger) : IRequestHandler<CreateUserCommand>
    {
        private readonly IUsersRepository _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly ILogger<CreateUserHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Si procede alla creazione dell'utente {command}", command);
            bool existingMail = await _userRepository.CheckExistingEmail(command.Email);

            if (existingMail)
            {
                throw new UserConflictException($"L'email {command.Email} è gia in uso");
            }

            await _userRepository.CreateUser(command);
            logger.LogInformation("Utente creato con successo {command}", command);
        }
    }
}
