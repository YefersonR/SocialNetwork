using AutoMapper;
using Core.Application.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class CommentService : GenericService<Comment,CommentSaveViewModel,CommentViewModel>,ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository,IMapper mapper):base(commentRepository,mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            
        }

        public async Task<List<CommentViewModel>> GetAllViewModelWithInclude()
        {
            List<Comment> list = await _commentRepository.GetAllWithInclude(new List<string>{ "" });
            return list.Select(comment => new CommentViewModel
            {
             
                IdUser = comment.IdUser,
                Content = comment.Content,
                IdPost = comment.IdPost,
                 
            }).ToList();
        }
    }
}
