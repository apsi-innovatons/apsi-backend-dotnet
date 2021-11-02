using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apsi.backend.social.Models;
using Apsi.Database;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apsi.backend.social.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        //For authorization testing purposes
        [Authorize(Roles="Admin,Committee")]
        [HttpGet("Users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers(string name = "", int page = 0, int count = 20)
        {
            return await _context.Users.Where(x => x.Username.Contains(name ?? ""))
                .OrderBy(x => x.Username)
                .Skip(count * page).Take(count)
                .ProjectToType<UserDto>()
                .ToListAsync();
        }
    }
}