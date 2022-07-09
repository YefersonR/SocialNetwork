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
    public class CommentService : GenericService<Comment,CommentSaveViewModel,CommentViewModel>,ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostServices _postRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IPostServices postRepository, IHttpContextAccessor httpContext, IMapper mapper):base(commentRepository,mapper)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");

        }
        public override async Task<CommentSaveViewModel> Add(CommentSaveViewModel type)
        {
            type.IdUser = user.Id;

            return await base.Add(type);
        }

        public async Task<List<CommentViewModel>> GetAllViewModelWithInclude()
        {

            List<Comment> list = await _commentRepository.GetAllWithInclude(new List<string>{ "User, Post" });
            return list.Select(comment => new CommentViewModel
            {

                Id = comment.Id,
                Content = comment.Content,
                IdUser = comment.IdUser,
                User = user,
                IdPost = comment.IdPost,
                



            }).ToList();
        }
    }
}
