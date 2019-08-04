using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReactMVCLogin.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Username cannot be empty")]
        [StringLength(12,MinimumLength =4,ErrorMessage ="Username should be 4-12 alpahnumeric")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Password should be 6-12 alpahnumeric")]
        public string Password { get; set; }

        public string Team { get; set; }

        
    }
}