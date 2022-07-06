using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        private readonly SocialMediaContext _mediaContext;
        public UserRepository(SocialMediaContext mediaContext) : base(mediaContext)
        {
            _mediaContext = mediaContext;
        }
        public override async Task Add(User user)
        {
            user.Password = PasswordEncrytion.Encriptation(user.Password);
            await base.Add(user);
        }
        public async Task<User> Login(LoginViewModel login)
        {
            login.Password = PasswordEncrytion.Encriptation(login.Password);
            User user = await _mediaContext.Set<User>()
                .FirstOrDefaultAsync(us => us.UserName == login.UserName && us.Password == login.Password);

            return user;
        }
    }
}
