using System.Threading.Tasks;
using apsi.backend.social.Models;

namespace apsi.backend.social.Services
{
    public interface IUserService
    {
        Task<LoggedUserDto> Authenticate(string username, string password);
    }
}