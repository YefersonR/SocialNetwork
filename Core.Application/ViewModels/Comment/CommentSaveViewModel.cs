using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Comment
{
    public class CommentSaveViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "No puede hacer una publicacion vacia")]
        public string Content { get; set; }
        public int IdUser { get; set; }
        public UserViewModel User { get; set; }
        public int IdPost { get; set; }
        public PostViewModel Post { get; set; }

    }
}
