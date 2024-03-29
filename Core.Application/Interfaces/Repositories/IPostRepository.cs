﻿using Core.Application.ViewModels.Post;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface IPostRepository :IGenericRepository<Post>
    {
        Task<Post> UpdatePost(Post post, int id);
    }
}
