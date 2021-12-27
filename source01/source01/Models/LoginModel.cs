﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace source01.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}