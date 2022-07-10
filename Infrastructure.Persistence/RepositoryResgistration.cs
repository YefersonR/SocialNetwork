using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class RepositoryResgistration
    {
        public static void AddRepositories(this IServiceCollection service, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("DatabaseInMemory"))
            {
                service.AddDbContext<SocialMediaContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
            }
            else
            {
               service.AddDbContext<SocialMediaContext>(option => option.UseSqlServer(configuration.GetConnectionString("MediaConnectionString"), m => m.MigrationsAssembly(typeof(SocialMediaContext).Assembly.FullName)));
            }
            service.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            service.AddTransient<IPostRepository,PostRepositories>();
            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<ICommentRepository, CommentRepository>();
            service.AddTransient<IFriendRepository, FriendRepository>();


        }
    }
}
