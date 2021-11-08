using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using apsi.backend.social.Models;
using apsi.backend.social.Services;
using Apsi.Database;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apsi.backend.social.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public AuthController(IUserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult<LoggedUserDto>> Authenticate([FromBody] AuthenticateDto userDto)
        {
            var user = await _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet("GetLoggedUser")]
        public async Task<ActionResult<UserDto>> GetLoggedUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            if(userId == 0)
                return Unauthorized(new { message = "No logged user" });

            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (user == null)
                return BadRequest(new { message = "User not found in database" });

            return user.Adapt<UserDto>();
        }
    }
}