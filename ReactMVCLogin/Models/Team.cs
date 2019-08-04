using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReactMVCLogin.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; }
    }
}