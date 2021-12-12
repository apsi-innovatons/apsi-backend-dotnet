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


        public async Task<PostDto> GetPostById(int id)
        {
            return await _context.Posts.Where(x => x.Id.Equals(id))
                .OrderBy(x => x.Author.Username)
                .ProjectToType<PostDto>()
                .FirstOrDefaultAsync();
        }
        public async Task<Post> GetPostByIdDb(int id)
        {
            return await _context.Posts.Where(x => x.Id.Equals(id))
                .OrderBy(x => x.Author.Username)
                .FirstOrDefaultAsync();
        }


        public async Task<int?> CreatePostAnswer(CreatePostAnswerDto postAnswer, User user)
        {
            var post = await GetPostByIdDb(postAnswer.PostId);
            if(post != null)
            {
                var answer = new PostAnswer();
                answer.Id = null;
                answer.Author = user;
                answer.Text = postAnswer.Text;

                await _context.PostAnswers.AddAsync(answer);
                await _context.SaveChangesAsync();

                if (post.PostAnswers == null) post.PostAnswers = new List<PostAnswer>();
                post.PostAnswers.Add(answer);
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return answer.Id;
            }
            else
            {
                return null;
            }
        }

        public async Task<int?> DeletePostById(int id)
        {
            var post = await GetPostByIdDb(id);
            if(post == null)
            {
                return null;
            }
            else
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return id;
            }
        }

        public async Task<List<PostDto>> GetPostsByTitle(StringPagingDto titlePaging)
        {
            return await _context.Posts.Where(x => x.Title.Contains(titlePaging.String))
                .OrderBy(x => x.Id)
                .Skip(titlePaging.count * titlePaging.page).Take(titlePaging.count)
                .ProjectToType<PostDto>()
                .ToListAsync();
        }

        public async Task<List<PostDto>> GetPostsByText(StringPagingDto textPaging)
        {
            return await _context.Posts.Where(x => x.Text.Contains(textPaging.String))
                .OrderBy(x => x.Id)
                .Skip(textPaging.count * textPaging.page).Take(textPaging.count)
                .ProjectToType<PostDto>()
                .ToListAsync();
        }

        public async Task<List<PostDto>> GetPostsByAnswerText(StringPagingDto textPaging)
        {
            var posts = await GetAll(textPaging);
            var postsWithAnswerText = new List<PostDto>();

            foreach (PostDto post in posts)
            {
                foreach(PostAnswerDto answer in post.PostAnswers)
                {
                    if (answer.Text.Contains(textPaging.String))
                    {
                        postsWithAnswerText.Add(post);
                        break;
                    }
                }
            }
            return postsWithAnswerText;
        }

        public async Task<List<PostDto>> GetPostsByAnswerAuthor(AuthorPagingDto authorPaging)
        {
            var posts = await GetAll(authorPaging);
            var postsWithAnswerAuthor = new List<PostDto>();

            foreach (PostDto post in posts)
            {
                foreach(PostAnswerDto answer in post.PostAnswers)
                {
                    if (answer.Author.Username.Contains(authorPaging.AuthorUsername))
                    {
                        postsWithAnswerAuthor.Add(post);
                        break;
                    }
                }
            }

            return postsWithAnswerAuthor;
        }
    }
}
