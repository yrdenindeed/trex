using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using trex.Models;
using trex.Utils;

namespace trex.Controllers
{
    public class SecurityController : Controller
    {

        private TrexContext db = new TrexContext();

    
        void CreateDefaultUser()
        {
            User u = new User()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.com",
                HashedPassword = "1000:lsQrY6Ua0RhOY87r138ykAQnorrHztoP:2zyrQOoEfGxWZJbM/2fhuE9euXPIhG+n"
            };
            db.Users.Add(u);
            db.SaveChanges();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string inputEmail, string inputPassword, bool rememberMe)
        {
         
            if (!string.IsNullOrWhiteSpace(inputEmail) && !string.IsNullOrWhiteSpace(inputPassword))
            {
                string email = inputEmail.ToString();
                var user = this.db.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    if (PasswordHash.ValidatePassword(inputPassword, user.HashedPassword))
                    {
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(), rememberMe);
                        return RedirectToAction("Index", "Ticket");
                    }
                }
            }
            ViewBag.Message = "Incorrect username or password.";
            
            return View();
        }
	}
}