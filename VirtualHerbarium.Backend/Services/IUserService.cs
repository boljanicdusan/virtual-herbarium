using System.Threading.Tasks;
using VirtualHerbarium.Backend.DTOs;

namespace VirtualHerbarium.Backend.Services
{
    public interface IUserService
    {
        Task<UserDto> AuthenticateUser(UserDto login);
        Task<UserDto> CreateUser(UserDto input);
    }
}