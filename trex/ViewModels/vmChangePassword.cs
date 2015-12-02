using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using trex.Models;

namespace trex.ViewModels
{
    public class vmChangePassword 
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public vmChangePassword()
        {
            User = new User();
        }
    }
}