using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Commands;
using Users.Application.CustomExceptions;
using Users.Application.Repositories;
using Users.Application.Responses;

namespace Users.Application.Handlers
{
    public class UpdateUserHandler(IUsersRepository usersRepository, ILogger<UpdateUserHandler> logger) : IRequestHandler<UpdateUserCommand, UserResponseModel>
    {
        private readonly IUsersRepository _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        private readonly ILogger<UpdateUserHandler> _logger = logger ?? throw new ArgumentNullException(nameof(usersRepository));

        public async Task<UserResponseModel> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Si procede alla modifica dell'utente con IdUtente {IdUtente}", command.IdUtente);

            _ = await _usersRepository.GetUserById(command.IdUtente)
                ?? throw new UserNotFoundException("L'utente non può essere modificato perchè non esiste");

            UserResponseModel? userUpdated = await _usersRepository.UpdateUser(command);

            _logger.LogInformation("Utente con {IdUtente} modificato con successo", command.IdUtente);

            return userUpdated!;
        }
    }
}
