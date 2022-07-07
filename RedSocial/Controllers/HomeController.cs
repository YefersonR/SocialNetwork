using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels;
using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedSocial.Middleware;
using RedSocial.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidateSession _validateSession;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;
        private readonly IPostServices _postServices;
        private readonly IUserService _userServices;
        private readonly ICommentService _commentServices;
        private readonly UploadImages _upload;
        private HomeViewModel _homeViewModel;


        public HomeController
        (ValidateSession validateSession, IHttpContextAccessor httpContext,IPostServices postServices, IUserService userServices,ICommentService commentService)
        {
            _validateSession = validateSession;
            _httpContext = httpContext;
            _postServices=postServices; 
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            _userServices = userServices;
            _commentServices = commentService;
            _upload = new();
            _homeViewModel = new();

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CreatePost = new PostSaveViewModel();
            if (_validateSession.HasUser())
            {
                _homeViewModel.postViewModels =  await _postServices.GetAllViewModelWithInclude();
                _homeViewModel.commentViewModels = await _commentServices.GetAllViewModelWithInclude();

                return View(_homeViewModel);
            }
            return RedirectToRoute(new { controller = "User", action = "Index"});
        }
        public async Task<IActionResult> Friends()
        {
            if (_validateSession.HasUser())
            {
                if (user.IsActiveUser)
                {
                    _homeViewModel.postViewModels = await _postServices.GetAllFriendsViewModels();
                    return View(_homeViewModel);
                }
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public async Task<IActionResult> CreatePost(PostSaveViewModel postSaveViewModel)
        {

            PostSaveViewModel postSVM = await _postServices.Add(postSaveViewModel);
            if (postSVM != null && postSVM.Id != 0)
            {
                postSVM.postImg = _upload.UploadImage(postSaveViewModel.File, postSVM.Id, "Posts",true);
                await _postServices.Update(postSVM, postSVM.Id);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentSaveViewModel commentSaveViewModel, int id)
        {
            await _commentServices.Add(commentSaveViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public async Task<IActionResult> CreateCommentFriends(CommentSaveViewModel commentSaveViewModel, int id)
        {
            await _commentServices.Add(commentSaveViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Friends" });
        }
        public async Task<IActionResult> Search(SearchFriendViewModel searchFriend)
        {
            return View(await _userServices.SearchFriend(searchFriend));
        }
    }
}
