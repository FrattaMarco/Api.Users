using MediatR;
namespace Users.Application.Queries
{
    public class CheckUserByEmailQuery : IRequest<bool>
    {
        public string Email { get; init; } = null!;
    }
}
