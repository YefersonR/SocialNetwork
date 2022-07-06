using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.Post;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<CommentSaveViewModel,CommentViewModel>
    {
        Task<List<CommentViewModel>> GetAllViewModelWithInclude();
    }
}
