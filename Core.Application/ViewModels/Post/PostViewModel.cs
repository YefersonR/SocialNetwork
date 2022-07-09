using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public string Content { get; set; }
        public string postImg { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public DateTime Created { get; set; }



    }
}
