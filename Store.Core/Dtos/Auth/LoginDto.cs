using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Core.Dtos.Auth
{
    public class LoginDto
    {

        [Required (ErrorMessage ="Email is Required!")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
    }
}
