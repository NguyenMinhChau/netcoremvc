﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPNP_WebAPI_COMP106401.EF
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
    }
}