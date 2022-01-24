using apsi.backend.social.Models;
using Apsi.Database;
using Apsi.Database.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Services
{
    public class SocialGroupService : ISocialGroupService
    {
        private readonly AppDbContext _context;

        public SocialGroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(SocialGroupDto socialGroup)
        {
            var dbGroup = socialGroup.Adapt<SocialGroup>();
            dbGroup.Id = null;
            _context.SocialGroups.Add(dbGroup);
            _context.SaveChanges();
            return dbGroup.Id;
        }
        public async Task<SocialGroupIdDto> GetById(int id)
        {
            return await _context.SocialGroups.Where(x => x.Id.Equals(id))
                                              .ProjectToType<SocialGroupIdDto>()
                                              .FirstOrDefaultAsync();
        }
        public async Task<SocialGroup> GetByIdDb(int id)
        {
            return await _context.SocialGroups.Where(x => x.Id.Equals(id))
                                              .ProjectToType<SocialGroup>()
                                              .FirstOrDefaultAsync();
        }

        public async Task<List<SocialGroupIdDto>> GetByName(SocialGroupPagingDto socialGroupPaging)
        {
            return await _context.SocialGroups.Where(x => x.Name.Contains(socialGroupPaging.Name))
                .OrderBy(x => x.Id)
                .Skip(socialGroupPaging.count * socialGroupPaging.page).Take(socialGroupPaging.count)
                .ProjectToType<SocialGroupIdDto>()
                .ToListAsync();
        }

        public async Task<List<SocialGroupIdDto>> GetAll(PagingDto paging)
        { 
            return await _context.SocialGroups
                .OrderBy(x => x.Id)
                .ProjectToType<SocialGroupIdDto>()
                .ToListAsync();
        }

        public async Task<SocialGroup> GetDbDataByName(string socialGroupName)
        {
            return await _context.SocialGroups.Where(x => x.Name.Equals(socialGroupName)).FirstOrDefaultAsync();
        }

        public async Task<int?> Update(SocialGroupDto socialGroup)
        {
            throw new NotImplementedException();
        }
    }
}
