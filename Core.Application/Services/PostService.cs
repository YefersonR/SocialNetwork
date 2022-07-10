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
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, ICommentRepository commentRepository, IHttpContextAccessor httpContext, IMapper mapper):base(postRepository,mapper)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
        }
        public override async Task<PostSaveViewModel> Add(PostSaveViewModel type)
        {
            type.UserId = user.Id;
            return await base.Add(type);
        }
       

        public async Task<List<PostViewModel>> GetAllViewModelWithInclude()
        {
            List<Comment> comments = await _commentRepository.GetAllWithInclude(new List<string> { "User" });
            List<CommentViewModel> commentViewModels = _mapper.Map<List<CommentViewModel>>(comments);
            List<Post> list = await _postRepository.GetAllWithInclude(new List<string> { "User" });
            return list.Where(post => post.UserId == user.Id).OrderByDescending(post => post.Created).Select(post => new PostViewModel
            {
                Id = post.Id,
                UserId = post.UserId,
                Content = post.Content,
                postImg = post.postImg,
                User = user,
                Comments = commentViewModels.Where(comment=>comment.IdPost == post.Id).ToList(),
                Created = post.Created
                
            }).ToList();
        }
    }
}
