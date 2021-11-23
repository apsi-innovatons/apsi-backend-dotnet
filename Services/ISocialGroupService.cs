using apsi.backend.social.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Services
{
    public interface ISocialGroupService
    {
        Task<List<SocialGroupIdDto>> GetAll(PagingDto paging);
        Task<List<SocialGroupIdDto>> Get(SocialGroupPagingDto socialGroupPaging);
        Task<int?> Create(SocialGroupDto socialGroup);
        Task<int?> Update(SocialGroupDto socialGroup);
        Task<int> Delete(string socialGroupName);
    }
}
