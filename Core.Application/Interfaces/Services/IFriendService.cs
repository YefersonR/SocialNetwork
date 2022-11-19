using Core.Application.ViewModels.Friends;
using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IFriendService
    {
        Task Add(FriendSaveViewModel friends);
        Task Delete(int id);
        Task<List<UserViewModel>> GetAllViewModels();
        Task<List<PostViewModel>> GetAllFriendsViewModels();

    }
}
