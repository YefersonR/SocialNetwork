using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.Friends;
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
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _frienRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly UserViewModel user;
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;
        public FriendService(IFriendRepository friendRepository, ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _frienRepository = friendRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");


        }
        public async Task Add(FriendSaveViewModel friendViewModel)
        {
            Friends friends = _mapper.Map<Friends>(friendViewModel);
            await _frienRepository.Add(friends);

        }
        public  async Task<List<UserViewModel>> GetAllViewModels()
        {
            List<User> users = await _userRepository.GetAll();
            List<Friends> friends = await _frienRepository.GetAll();

            var result = (from u in users
                          join f in friends
                          on u.Id equals f.IdFriend 
                          where(f.IdUser == user.Id)
                          select u).ToList();

            List<UserViewModel> viewModels = _mapper.Map<List<UserViewModel>>(result);
            return viewModels;
        }
        public virtual async Task<List<PostViewModel>> GetAllFriendsViewModels()
        {
            List<Comment> comments = await _commentRepository.GetAllWithInclude(new List<string> { "User" });
            List<CommentViewModel> commentViewModels = _mapper.Map<List<CommentViewModel>>(comments);
            List<Post> post = await _postRepository.GetAllWithInclude(new List<string> { "User" });
            List<User> users = await _userRepository.GetAll();
            List<Friends> friends = await _frienRepository.GetAll();

            var result = (from f in friends
                          join p in post
                          on f.IdFriend equals p.UserId
                          where (f.IdUser == user.Id)
                          select p).ToList();

            List<PostViewModel> viewModel = _mapper.Map<List<PostViewModel>>(result).ToList();
            return viewModel.Where(post=>post.UserId !=user.Id).OrderByDescending(post => post.Created).Select(post => new PostViewModel
            {
                Id = post.Id,
                UserId = post.UserId,
                Content = post.Content,
                postImg = post.postImg,
                User = post.User,
                Comments = commentViewModels.Where(comment => comment.IdPost == post.Id).ToList(),
                Created = post.Created


            }).ToList();

        }
    }
}
