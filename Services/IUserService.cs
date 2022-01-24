using System.Threading.Tasks;
using apsi.backend.social.Models;
using Apsi.Database.Entities;

namespace apsi.backend.social.Services
{
    public interface IUserService
    {
        Task<LoggedUserDto> Authenticate(string username, string password);
        Task<User> GetUserById(int id);
        Task<int?> AddSocialGroupToUser(User user, SocialGroup socialGroup);

    }
}