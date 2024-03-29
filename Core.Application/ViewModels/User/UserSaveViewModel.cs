﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre del usuario")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ingrese el apellido del usuario")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Ingrese el telefono del usuario")]
        [DataType(DataType.Text)]
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        [Required(ErrorMessage = "Debe ingresar el e-mail del usuario")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Debe ingresar nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña para el usuario")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Las contraseñas deben ser iguales")]
        [Required(ErrorMessage = "Debe ingresar una contraseña para el usuario")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool IsActiveUser { get; set; } = false;


    }
}
