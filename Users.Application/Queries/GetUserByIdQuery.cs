using MediatR;
using Users.Application.Responses;

namespace Users.Application.Queries
{
    public record GetUserByIdQuery : IRequest<UserResponseModel>
    {
        public int IdUtente { get; init; }
    }
}
