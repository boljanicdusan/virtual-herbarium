using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Entities;
using VirtualHerbarium.Backend.Helpers;

namespace VirtualHerbarium.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly VirtualHerbariumDbContext _context;
        private readonly IMapper _mapper;

        public UserService(VirtualHerbariumDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> AuthenticateUser(UserDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username && HashConverter.CreateMD5(login.Password) == u.Password);

            if (user != null)
            {
                return _mapper.Map<User, UserDto>(user);
            }

            return null;
        }

        public async Task<UserDto> CreateUser(UserDto input)
        {
            var user = new User
            {
                Username = input.Username,
                Password = HashConverter.CreateMD5(input.Password)
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<User, UserDto>(user);
        }
    }
}