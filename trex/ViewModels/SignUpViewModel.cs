using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using trex.Models;

namespace trex.ViewModels
{
    public class SignUpViewModel
    {
        public User User { get; set; }
        public string NewPassword { get; set; }
    }
}