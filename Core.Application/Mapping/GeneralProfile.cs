using AutoMapper;
using Core.Application.ViewModels.Comment;
using Core.Application.ViewModels.Friends;
using Core.Application.ViewModels.Post;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Comment, CommentViewModel>()
                .ReverseMap()
                .ForMember(comment => comment.Created, opt=>opt.Ignore())
                .ForMember(comment => comment.CreatedBy, opt => opt.Ignore())
                .ForMember(comment => comment.Updated, opt => opt.Ignore())
                .ForMember(comment => comment.UpdatedBy, opt => opt.Ignore());
            CreateMap<Comment, CommentSaveViewModel>()
                .ReverseMap()
                .ForMember(comment => comment.Created, opt => opt.Ignore())
                .ForMember(comment => comment.CreatedBy, opt => opt.Ignore())
                .ForMember(comment => comment.Updated, opt => opt.Ignore())
                .ForMember(comment => comment.UpdatedBy, opt => opt.Ignore());

            CreateMap<Post, PostViewModel>()
                .ReverseMap()
                .ForMember(comment => comment.CreatedBy, opt => opt.Ignore())
                .ForMember(comment => comment.Updated, opt => opt.Ignore())
                .ForMember(comment => comment.UpdatedBy, opt => opt.Ignore());

            CreateMap<Post, PostSaveViewModel>()
                .ReverseMap()
                .ForMember(comment => comment.Created, opt => opt.Ignore())
                .ForMember(comment => comment.CreatedBy, opt => opt.Ignore())
                .ForMember(comment => comment.Updated, opt => opt.Ignore())
                .ForMember(comment => comment.UpdatedBy, opt => opt.Ignore());

            CreateMap<User, UserViewModel>()
                .ReverseMap()
                .ForMember(comment => comment.Created, opt => opt.Ignore())
                .ForMember(comment => comment.CreatedBy, opt => opt.Ignore())
                .ForMember(comment => comment.Updated, opt => opt.Ignore())
                .ForMember(comment => comment.UpdatedBy, opt => opt.Ignore());

            CreateMap<User, UserSaveViewModel>()
                .ReverseMap()
                .ForMember(comment => comment.Created, opt => opt.Ignore())
                .ForMember(comment => comment.CreatedBy, opt => opt.Ignore())
                .ForMember(comment => comment.Updated, opt => opt.Ignore())
                .ForMember(comment => comment.UpdatedBy, opt => opt.Ignore());
            
            
            CreateMap<Friends, FriendSaveViewModel>()
                .ReverseMap();

            CreateMap<Friends, FriendViewModel>()
                .ReverseMap();



        }
    }
}
