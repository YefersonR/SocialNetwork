using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class PostService : GenericService<Post,PostSaveViewModel,PostViewModel>, IPostServices
    {
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IHttpContextAccessor httpContext, IMapper mapper):base(postRepository,mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
        }
        public override Task<PostSaveViewModel> Add(PostSaveViewModel type)
        {
            type.UserId = user.Id;
            return base.Add(type);
        }
        public async Task<List<PostViewModel>> GetAllViewModelWithInclude()
        {
            List<Post> list = await _postRepository.GetAllWithInclude(new List<string> { "" });
            return list.Select(post => new PostViewModel
            {
               UserId = post.UserId,
               Content = post.Content,

                
            }).ToList();
        }
    }
}
