using RestApiSample.Api.Data.DTOs;
using RestApiSample.Api.Data.Models;

namespace RestApiSample.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto?> AuthenticateUser(AuthenticateUserDto authenticateUserDto);

        Task<bool> IsUserExistsByUserName(string userName);

        Task RegisterUser(AuthenticateUserDto authenticateUserDto);
    }
}
