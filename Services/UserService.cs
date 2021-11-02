using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using apsi.backend.social.Models;
using Apsi.Database;
using Apsi.Database.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace apsi.backend.social.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _appSettings;
        private readonly AppDbContext _context;

        public UserService(AppDbContext context, IConfiguration appSettings)
        {
            _appSettings = appSettings;
            _context = context;
        }

        public async Task<LoggedUserDto> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;

            if (user.Password != password)
                return null;

            var userResult = user.Adapt<LoggedUserDto>();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings["JwtSecret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userResult.Token = tokenHandler.WriteToken(token);
            userResult.TokenExpirationDate = token.ValidTo;

            return userResult;
        }
    }
}