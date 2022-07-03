using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public int IdUser { get; set; }
        public UserViewModel User { get; set; }
        public int IdPost { get; set; }
        public PostViewModel Post { get; set; }

    }
}
