using Users.Application.Commands;
using Users.Application.Models;
using Users.Application.Queries;
using Users.Application.Responses;

namespace Users.Application.Repositories
{
    public interface IUsersRepository
    {
        Task<UserModel?> GetUserById(int  idUtente);
        Task<bool> CheckExistingEmail(string email);
        Task<bool> CreateUser(CreateUserCommand createUserCommand);
        Task<bool> DeleteUser(DeleteUserCommand deleteUserCommand);
        Task<UserModel> UpdateUser(UpdateUserCommand userToUpdate);
        Task<IEnumerable<UserResponseModel>> GetAllUsers(GetAllUsersQuery getAlluUsersQuery);
    }
}
