using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    class FriendRepository : IFriendRepository
    {
        private readonly SocialMediaContext _mediaContext;
        public FriendRepository(SocialMediaContext mediaContext)
        {
            _mediaContext = mediaContext;
        }
        public async Task<Friends> Add(Friends friends)
        {
            await _mediaContext.Set<Friends>().AddAsync(friends);
            await _mediaContext.SaveChangesAsync();
            return friends;
        }
        public async Task Delete(Friends friends)
        {
            _mediaContext.Set<Friends>().Remove(friends);
            await _mediaContext.SaveChangesAsync();
        }
        public async Task<List<Friends>> GetAll()
        {
            return await _mediaContext.Set<Friends>().ToListAsync();
        }
        
    }
}
