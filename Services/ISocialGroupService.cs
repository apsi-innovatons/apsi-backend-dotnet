using apsi.backend.social.Models;
using Apsi.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Services
{
    public interface ISocialGroupService
    {
        Task<SocialGroupIdDto> GetById(int id);
        Task<SocialGroup> GetByIdDb(int id);
        Task<List<SocialGroupIdDto>> GetAll(PagingDto paging);
        Task<List<SocialGroupIdDto>> GetByName(SocialGroupPagingDto socialGroupPaging);
        Task<SocialGroup> GetDbDataByName(string socialGroupName);
        Task<int?> Create(SocialGroupDto socialGroup);
        Task<int?> Update(SocialGroupDto socialGroup);
    }
}
