using apsi.backend.social.Models;
using Apsi.Database;
using Apsi.Database.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int?> CreatePost(CreatePostDto post, User user, SocialGroup socialGroup)
        {

            var dbPost = new Post
            {
                Id = null,
                Title = post.Title,
                Text = post.Text,
                SocialGroup = socialGroup,
                Author = user
            };

            _context.Posts.Add(dbPost);
            await _context.SaveChangesAsync();

            return dbPost.Id;
        }

        public async Task<List<PostDto>> GetAll(PagingDto paging)
        {
            return await _context.Posts
                .OrderBy(x => x.Id)
                .Skip(paging.count * paging.page).Take(paging.count)
                .ProjectToType<PostDto>()
                .ToListAsync();
        }

        public async Task<List<PostDto>> GetPostsByAuthor(AuthorPagingDto authorPaging)
        {
            return await _context.Posts.Where(x => x.Author.Username.Equals(authorPaging.AuthorUsername))
                .OrderBy(x => x.Id)
                .Skip(authorPaging.count * authorPaging.page).Take(authorPaging.count)
                .ProjectToType<PostDto>()
                .ToListAsync();
        }

        public async Task<PostDto> GetPostById(IdPagingDto idPaging)
        {
            return await _context.Posts.Where(x => x.Id.Equals(idPaging.Id))
                .OrderBy(x => x.Author.Username)
                .Skip(idPaging.count * idPaging.page).Take(idPaging.count)
                .ProjectToType<PostDto>()
                .FirstOrDefaultAsync();
        }
    }
}
