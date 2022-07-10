using Core.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IPostServices : IGenericService<PostSaveViewModel,PostViewModel>
    {
        Task<List<PostViewModel>> GetAllViewModelWithInclude();
    }
}
