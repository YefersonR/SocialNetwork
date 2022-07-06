using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PostRepositories : GenericRepository<Post>, IPostRepository
    {
        private readonly SocialMediaContext _mediaContext;
        public PostRepositories(SocialMediaContext mediaContext):base(mediaContext)
        {
            _mediaContext = mediaContext;
        }
    }
}
