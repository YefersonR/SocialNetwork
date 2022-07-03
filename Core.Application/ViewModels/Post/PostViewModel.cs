using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public string Content { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }

    }
}
