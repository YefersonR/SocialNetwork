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

namespace RedSocial.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateSession _validateSession;
        public UserController(IUserService userService,ValidateSession validateSession)
        {
            _userService = userService;
            _validateSession = validateSession;
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
            await _userService.Add(saveViewModel);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        } 
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

    }
}
