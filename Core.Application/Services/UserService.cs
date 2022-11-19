using AutoMapper;
using Core.Application.Dtos.Email;
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
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper,IEmailService emailService):base(userRepository,mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;


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
  
        public override async Task<UserSaveViewModel> Add(UserSaveViewModel type)
        {
            var users = await _userRepository.GetAll();
            users = users.Where(user => user.UserName == type.UserName).ToList();
            if(users.Count == 0)
            {

                UserSaveViewModel userSaveViewModel = await base.Add(type);
                await _emailService.SendEmail(new EmailRequest
                {
                    To = userSaveViewModel.Mail,
                    Subject ="Confirmacion de Email",
                    Body = $"Entra al link para activar tu cuenta https://localhost:5001/User/ConfirmUser/{userSaveViewModel.Id}"
                });
                return userSaveViewModel;
             }
            return null;
        }
        public override Task Update(UserSaveViewModel type, int id)
        {
            
            return base.Update(type, id);
        }
        public async Task ConfirmMail(UserSaveViewModel userVm)
        {
            User user = _mapper.Map<User>(userVm);
            user.IsActiveUser = true;
            await _userRepository.Update(user,user.Id);
        }
        public async Task<List<UserViewModel>> SearchFriend(SearchFriendViewModel searchViewModel)
        {
            var list = await _userRepository.GetAll();
            return list.Where(user => user.UserName.ToLower().Contains(searchViewModel.userName.ToLower())).Select(user => new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                LastName =user.LastName,
                Phone = user.Phone,
                ProfilePicture = user.ProfilePicture,
                Mail = user.Mail,
                UserName = user.UserName,
                Password = user.Password

            }).ToList();
        }

        public async Task ChangePassword(string UserName)
        {
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u=>u.UserName == UserName);
            if (user != null)
            {
                user.Password = GeneratePassword();
                var pass = user.Password;
                await _userRepository.Update(user, user.Id);
                EmailRequest email = new();
                email.Subject = "Password changed";
                email.To = user.Mail;
                email.Body = $"Your password has been changed! you new password is {pass}";
                await _emailService.SendEmail(email);

            }
        }
        public async Task<bool> ExistUser(string UserName)
        {
            var list = await _userRepository.GetAll();
            var user = list.FirstOrDefault(user => user.UserName == UserName);
            if(user != null)
            {
                return true;
            }

            return false;
        }
        public string GeneratePassword()
        {
            int length = 12;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                res.Append(valid[random.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
