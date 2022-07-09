using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;


namespace Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int IdUser { get; set; }
        public UserViewModel User { get; set; }
        public int IdPost { get; set; }
        public PostViewModel Post { get; set; }

    }
}
