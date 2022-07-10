using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.Friends;
using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels
{
    public class HomeViewModel
    {
        public PostSaveViewModel postSaveViewModel{ get; set; }
        public List<PostViewModel> postViewModels { get; set; }
        public CommentSaveViewModel commentSaveViewModel{ get; set; }
        public List<CommentViewModel> commentViewModels { get; set; }
        public List<UserViewModel> friendsViewModels { get; set; }


    }
}
