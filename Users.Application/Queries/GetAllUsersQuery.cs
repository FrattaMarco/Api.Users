using MediatR;
using Users.Application.Responses;

namespace Users.Application.Queries
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserResponseModel>>
    {
    }
}
