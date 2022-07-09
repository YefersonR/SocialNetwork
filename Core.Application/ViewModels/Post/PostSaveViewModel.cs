using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Post
{
    public class PostSaveViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        [Required(ErrorMessage ="No puede hacer una publicacion vacia")]
        public string Content { get; set; }
        public string postImg { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
        public  List<CommentViewModel> Comments { get; set; }

    }
}
