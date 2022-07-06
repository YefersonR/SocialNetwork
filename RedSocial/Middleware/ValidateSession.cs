using Core.Application.Helpers;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.Middleware
{
    public class ValidateSession
    {
        private readonly IHttpContextAccessor _httpContext;
        public ValidateSession(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool HasUser()
        {
            UserViewModel user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
            if(user == null)
            {
                return false;
            }
            return true;
        }
    }
}
