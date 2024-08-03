using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Queries;
using Users.Application.Repositories;

namespace Users.Application.Handlers
{
    public class CheckUserByEmailHandler(IUsersRepository usersRepository, ILogger<CheckUserByEmailHandler> logger) : IRequestHandler<CheckUserByEmailQuery, bool>
    {
        private readonly IUsersRepository _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        private readonly ILogger<CheckUserByEmailHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<bool> Handle(CheckUserByEmailQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Check esistenza utente per Email: {Email}", request.Email);

            return await _usersRepository.CheckExistingEmail(request.Email);
        }
    }
}
