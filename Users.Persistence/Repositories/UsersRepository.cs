using DapperContext.Application.Repositories;
using System.Data;
using Users.Application.Commands;
using Users.Application.Models;
using Users.Application.Queries;
using Users.Application.Repositories;
using Users.Application.Responses;
using Users.Persistence.Dto;

namespace Users.Persistence.Repositories
{
    public class UsersRepository(IGenericRepository repository) : IUsersRepository
    {
        private readonly IGenericRepository _GenericRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        public async Task<bool> CheckExistingEmail(string email)
        {
            IEnumerable<UserModel> result = await _GenericRepository.QueryGetAsyncByStoredProcedure<UserModel>("GetUserByParameters",
               new
               {
                   Email = email
               }, CommandType.StoredProcedure);
            return result.SingleOrDefault() is not null;
        }

        public async Task<bool> CreateUser(CreateUserCommand createUserCommand)
        {
            bool result = await _GenericRepository.QueryAddAsyncByStoredProcedure("CreateUser",
                new
                {
                    createUserCommand.Nome,
                    createUserCommand.Cognome,
                    createUserCommand.Indirizzo,
                    createUserCommand.Email,
                    createUserCommand.DataNascita,
                    createUserCommand.CodiceFiscale,
                    createUserCommand.Password
                },
                CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> DeleteUser(DeleteUserCommand deleteUser)
        {
            bool result = await _GenericRepository.QueryDeleteAsyncByStoredProcedure("DeleteUser",
                new
                {
                    deleteUser.IdUtente
                },
                CommandType.StoredProcedure);

            return result;
        }

        public async Task<UserModel?> GetUserById(int idUtente)
        {
            IEnumerable<UserModel> users = await _GenericRepository.QueryGetAsyncByStoredProcedure<UserModel>("GetUserByParameters",
                new
                {
                    IdUtente = idUtente
                }, CommandType.StoredProcedure);
            return users.SingleOrDefault();
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllUsers(GetAllUsersQuery getAlluUsersQuery)
        {
            IEnumerable<UserDto> usersDto = await _GenericRepository.GetAllAsyncWithStoredProcedure<UserDto>("GetUsers", CommandType.StoredProcedure);

            IEnumerable<UserResponseModel> usersResponseModels = usersDto.Select(x => new UserResponseModel
            {
                Nome = x.Nome,
                Cognome = x.Cognome,
                DataNascita = x.DataNascita,
                Email = x.Email,
                IdUtente = x.IdUtente,
                Indirizzo = x.Indirizzo,
                CodiceFiscale = x.CodiceFiscale
            }).ToList();

            return usersResponseModels;
        }

        public async Task<UserModel> UpdateUser(UpdateUserCommand userToUpdate)
        {
            UserModel user = await _GenericRepository.QueryUpdateAsyncByStoredProcedure<UserModel>("UpdateUserById", new
            {
                userToUpdate.IdUtente,
                userToUpdate.Body.Email,
                userToUpdate.Body.Nome,
                userToUpdate.Body.Cognome,
                userToUpdate.Body.CodiceFiscale,
                userToUpdate.Body.Indirizzo,
                userToUpdate.Body.DataNascita,
                userToUpdate.Body.Password,
            }, CommandType.StoredProcedure);

            return user;
        }
    }
}
