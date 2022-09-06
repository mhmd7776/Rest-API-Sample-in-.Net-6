using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestApiSample.Api.Data.DbContext;
using RestApiSample.Api.Data.DTOs;
using RestApiSample.Api.Data.Models;
using RestApiSample.Api.Repositories.Interfaces;

namespace RestApiSample.Api.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        private readonly SecretKeys _keys;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IOptions<SecretKeys> keys, IMapper mapper)
        {
            _context = context;
            _keys = keys.Value;
            _mapper = mapper;
        }

        #endregion

        public async Task<UserDto?> AuthenticateUser(AuthenticateUserDto authenticateUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Password!.Equals(authenticateUserDto.Password) && u.UserName!.ToLower().Trim().Equals(authenticateUserDto.UserName!.ToLower().Trim()));

            // user not found
            if (user == null)
            {
                return null;
            }

            #region Generate new token

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_keys.AuthenticationSecretKey!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.UserName!),
                    new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                })
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            #endregion

            var userDto = _mapper.Map<UserDto>(user);

            userDto.Token = tokenHandler.WriteToken(token);

            return userDto;
        }

        public async Task<bool> IsUserExistsByUserName(string userName)
        {
            return await _context.Users.AnyAsync(s => s.UserName!.ToLower().Trim().Equals(userName.ToLower().Trim()));
        }

        public async Task RegisterUser(AuthenticateUserDto authenticateUserDto)
        {
            var user = new User
            {
                UserName = authenticateUserDto.UserName,
                Password = authenticateUserDto.Password
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
