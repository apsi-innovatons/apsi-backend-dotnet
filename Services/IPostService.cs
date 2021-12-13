using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apsi.backend.social.Models;
using Apsi.Database.Entities;

namespace apsi.backend.social.Services
{
    public interface IPostService
    {
        public Task<List<PostDto>> GetAll(PagingDto paging);
        public Task<PostDto> GetPostById(int id);
        public Task<Post> GetPostByIdDb(int id);
        public Task<List<PostDto>> GetPostsByAuthor(AuthorPagingDto authorPaging);
        public Task<List<PostDto>> GetPostsByTitle(StringPagingDto titlePaging);
        public Task<List<PostDto>> GetPostsByText(StringPagingDto textPaging);
        public Task<List<PostDto>> GetPostsByAnswerText(StringPagingDto textPaging);
        public Task<List<PostDto>> GetPostsByAnswerAuthor(AuthorPagingDto authorPaging);
        public Task<int?> CreatePost(CreatePostDto post, User user, SocialGroup socialGroup);
        public Task<int?> UpdatePost(UpdatePostDto post, SocialGroup socialGroup);
        public Task<int?> DeletePostById(int id);
        public Task<int> GetPostsCount();
        public Task<int?> GetPostAnswersCountByPostId(int id);
        public Task<PostAnswerDto> GetPostAnswerById(int id);
        public Task<PostAnswer> GetPostAnswerByIdDb(int id);
        public Task<int?> CreatePostAnswer(CreatePostAnswerDto postAnswer, User user);
        public Task<int?> UpdatePostAnswer(UpdatePostAnswerDto postAnswer);
        public Task<int?> DeletePostAnswerById(int id);
        
    }
}
