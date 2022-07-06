using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService : GenericService<User,UserSaveViewModel,UserViewModel>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper):base(userRepository,mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserViewModel> Login(LoginViewModel loginViewModel)
        {
            User user = await _userRepository.Login(loginViewModel);
            if(user == null)
            {
                return null;
            }
            UserViewModel userView = _mapper.Map<UserViewModel>(user);

            return userView;
        }
        public async Task<List<UserViewModel>> SearchFriend(SearchFriendViewModel searchViewModel)
        {
            var list = await _userRepository.GetAll();
            return list.Where(user => user.UserName == searchViewModel.userName).Select(user => new UserViewModel
            {
                Name = user.Name,
                LastName =user.LastName,
                Phone = user.Phone,
                ProfilePicture = user.ProfilePicture,
                Mail = user.Mail,
                UserName = user.UserName,
                Password = user.Password

            }).ToList();
        }
    }
}
