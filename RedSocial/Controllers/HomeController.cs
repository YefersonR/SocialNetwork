using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels;
using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.Friends;
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
using System.IO;
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
        private readonly IFriendService _FriendServices;
        FriendSaveViewModel friendSaveViewModel;
        private readonly ICommentService _commentServices;
        private readonly UploadImages _upload;
        private HomeViewModel _homeViewModel;


        public HomeController
        (ValidateSession validateSession, IHttpContextAccessor httpContext,IPostServices postServices, IFriendService FriendServices, IUserService userServices,ICommentService commentService)
        {
            _validateSession = validateSession;
            _httpContext = httpContext;
            _postServices=postServices;
            _FriendServices = FriendServices;

            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            _userServices = userServices;
            _commentServices = commentService;
            _upload = new();
            _homeViewModel = new();
            friendSaveViewModel = new();

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CreatePost = new PostSaveViewModel();
            if (_validateSession.HasUser())
            {
                _homeViewModel.postViewModels =  await _postServices.GetAllViewModelWithInclude();
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
                    _homeViewModel.postViewModels = await _FriendServices.GetAllFriendsViewModels();
                    _homeViewModel.friendsViewModels = await _FriendServices.GetAllViewModels();
                    return View(_homeViewModel);
                }
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
        public async Task<IActionResult> CreatePost(PostSaveViewModel postSaveViewModel)
        {
            if (ModelState.IsValid)
            {
                PostSaveViewModel postSVM = await _postServices.Add(postSaveViewModel);
                if (postSVM != null && postSVM.Id != 0)
                {
                    postSVM.postImg = _upload.UploadImage(postSaveViewModel.File, postSVM.Id, "Posts",true);
                    await _postServices.Update(postSVM, postSVM.Id);
                }
                return RedirectToRoute(new { controller = "Home", action = "Index" });
             }
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentSaveViewModel commentSaveViewModel)
        {
            if (ModelState.IsValid)
            {
                await _commentServices.Add(commentSaveViewModel);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public async Task<IActionResult> CreateCommentFriends(CommentSaveViewModel commentSaveViewModel)
        {
            if (ModelState.IsValid)
            {
                await _commentServices.Add(commentSaveViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Friends" });
            }
            return null;
        }
        public async Task<IActionResult> Search(SearchFriendViewModel searchFriend)
        {
            if (ModelState.IsValid)
            {
                return View(await _userServices.SearchFriend(searchFriend));
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> AddFriend(int id)
        {

            var friend = await _userServices.GetById(id);
            friendSaveViewModel.IdUser = user.Id;
            friendSaveViewModel.IdFriend = friend.Id;
            await _FriendServices.Add(friendSaveViewModel);
            return RedirectToRoute(new { controller = "Home", action = "Friends" });
        }
            public async Task<IActionResult> Delete(int id)
        {
            await _postServices.Delete(id);
            string basePath = $"/Img/Anuncios/${id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }
                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public async Task<IActionResult> Edit(PostSaveViewModel postSaveViewModel)
        {
            await _postServices.Update(postSaveViewModel,postSaveViewModel.Id);
            return RedirectToRoute(new { controller = "Home", action = "Index"});
        }
    }
}
