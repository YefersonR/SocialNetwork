using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly SocialMediaContext _mediaContext;
        public UserRepository(SocialMediaContext mediaContext) : base(mediaContext)
        {
            _mediaContext = mediaContext;
        }
    }
}
