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
                Date = DateTime.UtcNow,
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
                .ProjectToType<PostDto>()
                .FirstOrDefaultAsync();
        }

        public async Task<PostAnswerDto> GetPostAnswerById(int id)
        {
            return await _context.PostAnswers.Where(x => x.Id.Equals(id))
                .ProjectToType<PostAnswerDto>()
                .FirstOrDefaultAsync();
        }

        public async Task<Post> GetPostByIdDb(int id)
        {
            return await _context.Posts.Where(x => x.Id.Equals(id))
                .ProjectToType<Post>()
                .FirstOrDefaultAsync();
        }
        public async Task<PostAnswer> GetPostAnswerByIdDb(int id)
        {
            return await _context.PostAnswers.Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<int?> CreatePostAnswer(CreatePostAnswerDto postAnswer, User user)
        {
            var post = await GetPostByIdDb(postAnswer.PostId);
            if(post != null)
            {
                var answer = new PostAnswer()
                {
                    Id = null,
                    Author = user,
                    Text = postAnswer.Text,
                    Date = DateTime.UtcNow
                };

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
        public async Task<int?> DeletePostAnswerById(int id)
        {
            var answer = await GetPostAnswerByIdDb(id);
            if(answer == null)
            {
                return null;
            }
            else
            {
                _context.PostAnswers.Remove(answer);
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

        public async Task<int> GetPostsCount()
        {
            return await _context.Posts.CountAsync();
        }


        public async Task<int?> GetPostAnswersCountByPostId(int id)
        {
            Post post = await GetPostByIdDb(id);
            if(post != null)
            {
                return post.PostAnswers.Count;
            }
            return null;
        }

        public async Task<int?> UpdatePost(UpdatePostDto post, SocialGroup socialGroup)
        {
            Post postDb = await GetPostByIdDb(post.PostId);
            if (postDb != null)
            {
                if(post.Title != null)
                {
                    postDb.Title = post.Title;
                }
                if (post.Text != null)
                { 
                    postDb.Text = post.Text;
                }
                if (post.socialGroupName != null)
                { 
                    postDb.SocialGroup = socialGroup;
                }

                _context.Update(postDb);
                await _context.SaveChangesAsync();
                return postDb.Id;
            }
            return null;
        }

        public async Task<int?> UpdatePostAnswer(UpdatePostAnswerDto postAnswer)
        {
            PostAnswer answerDb = await GetPostAnswerByIdDb(postAnswer.AnswerId);
            if(answerDb != null)
            {
                answerDb.Text = postAnswer.Text;
                _context.Update(answerDb);
                await _context.SaveChangesAsync();
                return answerDb.Id;
            }
            return null;
        }
    }
}
