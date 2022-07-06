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
    public class CommentRepository : GenericRepository<Comment>,ICommentRepository
    {
        private readonly SocialMediaContext _mediaContext;
        public CommentRepository(SocialMediaContext mediaContext): base(mediaContext)
        {
            _mediaContext = mediaContext;
        }
    }
}
