using apsi.backend.social.Models;
using apsi.backend.social.Services;
using Apsi.Database;
using Apsi.Database.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apsi.Backend.Social.Controllers
{

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SocialGroupController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ISocialGroupService _socialGroupService;
        public SocialGroupController(AppDbContext context, ISocialGroupService socialGroupService)
        {
            _context = context;
            _socialGroupService = socialGroupService;
        }

        [HttpGet("SocialGroups")]
        public async Task<ActionResult<List<SocialGroupIdDto>>> Get([FromQuery] SocialGroupPagingDto socialGroupPaging)
        {
            return await _socialGroupService.GetByName(socialGroupPaging);
        }

        [HttpGet("SocialGroupsAll")]
        public async Task<ActionResult<List<SocialGroupIdDto>>> GetAll([FromQuery] PagingDto paging)
        {
            return await _socialGroupService.GetAll(paging);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateSocialGroups")]
        public async Task<ActionResult<int>> Create(SocialGroupDto socialGroup)
        {
            int? id = await _socialGroupService.Create(socialGroup);
            if(id == null)
            {
                return BadRequest("Social group not created");
            }
            return Ok(id);
        }
    }
}
