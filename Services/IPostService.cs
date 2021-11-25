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
        public Task<List<PostDto>> GetPostsByAuthor(AuthorPagingDto authorPaging);
        public Task<PostDto> GetPostById(IdPagingDto idPaging);
        public Task<List<PostDto>> GetAll(PagingDto paging);
        public Task<int?> CreatePost(CreatePostDto post, User user, SocialGroup socialGroup);
    }
}
