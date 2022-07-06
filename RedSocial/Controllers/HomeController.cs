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

        public HomeController(ValidateSession validateSession)
        {
            _validateSession = validateSession;
        }

        public IActionResult Index()
        {
            if (_validateSession.HasUser())
            {
                return View();

            }
            TempData["mydata"] = "No tiene acesso";
            return RedirectToRoute(new { controller = "User", action = "Index"});
        }
        public IActionResult Friends()
        {
            if (_validateSession.HasUser())
            {
                return View();

            }
            TempData["mydata"] = "No tiene acesso";
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
