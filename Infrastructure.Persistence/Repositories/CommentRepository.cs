using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class CommentRepository :GenericRepository<CommentRepository>
    {
        private readonly SocialMediaContext _mediaContext;
        public CommentRepository(SocialMediaContext mediaContext): base(mediaContext)
        {
            _mediaContext = mediaContext;
        }
    }
}
