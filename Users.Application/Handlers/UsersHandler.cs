using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.CustomExceptions;
using Users.Application.Queries;
using Users.Application.Repositories;
using Users.Application.Responses;

namespace Users.Application.Handlers
{
    public class UsersHandler(IUsersRepository usersRepository, ILogger<UsersHandler> logger) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponseModel>>
    {
        private readonly IUsersRepository _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        private readonly ILogger<UsersHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<IEnumerable<UserResponseModel>> Handle(GetAllUsersQuery getAllUsersQuery, CancellationToken cancellationToken)
        {
            IEnumerable<UserResponseModel> usersResponse;

            _logger.LogInformation("Ricerca di tutti gli utenti in corso");
            usersResponse = await _usersRepository.GetAllUsers(getAllUsersQuery);

            return usersResponse.Any() ? usersResponse : throw new UserNotFoundException("Non è stato trovato nessun utente");
        }
    }
}