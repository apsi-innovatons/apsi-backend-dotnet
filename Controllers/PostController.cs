using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apsi.Database;
using Apsi.Database.Entities;
using apsi.backend.social.Models;
using apsi.backend.social.Services;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Apsi.Backend.Social.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ISocialGroupService _socialGroupService;
        private readonly AppDbContext _context;


        public PostController(IUserService userService, IPostService postService, ISocialGroupService socialGroupService, AppDbContext context)
        {
            _userService = userService;
            _socialGroupService = socialGroupService;
            _postService = postService;
            _context = context;
        }

        [HttpGet("GetPostsByAuthor")]
        public async Task<ActionResult<List<PostDto>>> GetPostsByAuthor([FromQuery] AuthorPagingDto authorPaging)
        {
            return await _postService.GetPostsByAuthor(authorPaging);
        }

        [HttpGet("GetPostById")]
        public async Task<ActionResult<PostDto>> GetPostById(int id)
        {
            return await _postService.GetPostById(id);
        }

        [HttpGet("GetPostsByTitle")]
        public async Task<ActionResult<List<PostDto>>> GetPostByTitle([FromQuery] StringPagingDto titlePaging)
        {
            return await _postService.GetPostsByTitle(titlePaging);
        }
        [HttpGet("GetPostsByText")]
        public async Task<ActionResult<List<PostDto>>> GetPostByText([FromQuery ]StringPagingDto textPaging)
        {
            return await _postService.GetPostsByText(textPaging);
        }
        [HttpGet("GetPostsByAnswerText")]
        public async Task<ActionResult<List<PostDto>>> GetPostsByAnswerText([FromQuery] StringPagingDto textPaging)
        {
            return await _postService.GetPostsByAnswerText(textPaging);
        }
        [HttpGet("GetPostsByAnswerAuthor")]
        public async Task<ActionResult<List<PostDto>>> GetPostsByAnswerAuthor([FromQuery] AuthorPagingDto authorPaging )
        {
            return await _postService.GetPostsByAnswerAuthor(authorPaging);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PostDto>>> GetAllPosts([FromQuery] PagingDto paging)
        {
            return await _postService.GetAll(paging);
        }

        [HttpPost("CreatePost")]
        public async Task<ActionResult<int>> CreatePost(CreatePostDto post)
        {
            var name = ClaimTypes.Name;
            if(name == null)
            {
                return BadRequest("Post not created, no user");
            }
            else
            {
                var dbUser = await _userService.GetUserById(int.Parse(HttpContext.User.Identity.Name));
                var dbSocialGroup = await _socialGroupService.GetDbDataByName(post.socialGroupName);;
                var postId = await _postService.CreatePost(post, dbUser, dbSocialGroup);
                if (postId == null)
                {
                    return BadRequest("Post not created");
                }
                else
                {
                    return Ok(postId);
                }
            }
        }

        [HttpPost("DeletePost")]
        public async Task<ActionResult<int>> DeletePostById(int id)
        {
            var result = await _postService.DeletePostById(id);
            return GetResultOrBadRequest(result, "Post to delete not found");
        }

        [HttpPost("CreatePostAnswer")]
        public async Task<ActionResult<int>> CreatePostAnswer(CreatePostAnswerDto postAnswer)
        {
            var name = ClaimTypes.Name;
            if(name == null)
            {
                return BadRequest("Post not created, no user");
            }
            else
            {
                var dbUser = await _userService.GetUserById(int.Parse(HttpContext.User.Identity.Name));
                var postAnswerId = await _postService.CreatePostAnswer(postAnswer, dbUser);
                if (postAnswerId == null)
                {
                    return BadRequest("Post not created");
                }
                else
                {
                    return Ok(postAnswerId);
                }
            }
        }

        [HttpPost("DeletePostAnswer")]
        public async Task<ActionResult<int>> DeletePostAnswerById(int id)
        {
            var result = await _postService.DeletePostById(id);
            return GetResultOrBadRequest(result, "Post to be deleted not found");

        }

        private ActionResult<int> GetResultOrBadRequest(int? result, string badRequestText)
        {
            if (result == null)
            {
                return BadRequest(badRequestText);
            }
            else
            {
                return result;
            }
        }
    }
}
