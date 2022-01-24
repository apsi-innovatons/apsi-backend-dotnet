using System.Collections.Generic;
using System.Linq;
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
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly ISocialGroupService _socialGroupService;

        public UsersController(AppDbContext context, IUserService userService, ISocialGroupService socialGroupService)
        {
            _context = context;
            _userService = userService;
            _socialGroupService = socialGroupService;
        }

        [Authorize(Roles="Admin,Committee")]
        [HttpGet("AddToSocialGroup")]
        public async Task<ActionResult<int>> AddToSocialGroup(int userId, int groupId)
        {
            var user = await _userService.GetUserById(userId);
            var socialGroup = await _socialGroupService.GetByIdDb(groupId);
            var result = await _userService.AddSocialGroupToUser(user, socialGroup);
            if(result != null)
            {
                return result;
            }
            else
            {
                return BadRequest("Could not add user to the social group");
            }
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