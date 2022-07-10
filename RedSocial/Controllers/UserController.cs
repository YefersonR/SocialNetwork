using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Application.Helpers;
using System.Threading.Tasks;
using RedSocial.Middleware;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RedSocial.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateSession _validateSession;
        private readonly UploadImages _upload;

        public UserController(IUserService userService, ValidateSession validateSession)
        {
            _userService = userService;
            _validateSession = validateSession;
            _upload = new();
        }
        public IActionResult Index(string message)
        {
            if (_validateSession.HasUser())
            {

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            User data = TempData["mydata"] as User;
                return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);

            }
            UserViewModel user = await _userService.Login(loginViewModel);
            if (user != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", user);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("UserValidation","Usuario o contraseña incorrectos");

            }
            return View(loginViewModel);
        }
        public IActionResult Register()
        {
            if (_validateSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }
            return View(new UserSaveViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveViewModel);

            }
            UserSaveViewModel userSaveView = await _userService.Add(saveViewModel);
            if (userSaveView != null)
            {

            if (userSaveView != null && userSaveView.Id != 0)
            {
                userSaveView.ProfilePicture = _upload.UploadImage(saveViewModel.PictureFile, userSaveView.Id,"Profiles",true);
                await _userService.Update(userSaveView, userSaveView.Id);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                ViewBag.Acceso = "Nombre se usuario en uso";
                return View();

            }
        } 
        public async Task<IActionResult> ConfirmUser(int id)
        {
              UserSaveViewModel user = await _userService.GetById(id);
              await _userService.ConfirmMail(user);
             return RedirectToRoute(new { controller = "User", action = "Index" });

        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

    }
}
