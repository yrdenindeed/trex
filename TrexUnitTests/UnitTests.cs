using System;
using System.Diagnostics;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trex.Controllers;
using trex.Models;
using System.Linq;

namespace TrexUnitTests
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod]
<<<<<<< HEAD
        public void TestMethodCreateNewUser()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                HashedPassword = ""
            };

            var uc = new UserController();
            Assert.IsTrue((uc.Create(user) as ViewResult).Model == user);
=======
        public void TestLoginSuccess()
        {
            var db = new TrexContext();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "a@a.com",
                HashedPassword = "1000:lsQrY6Ua0RhOY87r138ykAQnorrHztoP:2zyrQOoEfGxWZJbM/2fhuE9euXPIhG+n"
            };

            db.Users.Add(user);

            SecurityController c = new SecurityController();
            c.Login(user.Email, user.HashedPassword);
            
            
>>>>>>> 8c697cc99e6f858bd1dbe59b7a1645a0f8abc006
        }

        public void TestLoginSuccess()
        {
            var db = new TrexContext();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "a@a.com",
                HashedPassword = "1000:lsQrY6Ua0RhOY87r138ykAQnorrHztoP:2zyrQOoEfGxWZJbM/2fhuE9euXPIhG+n"
            };

            db.Users.Add(user);

            SecurityController c = new SecurityController();
            c.Login(user.Email, user.HashedPassword);
        }

        [TestMethod]
        public void TestMethodChangePasswordFromEmpty()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                HashedPassword = ""
            };

            Assert.IsTrue(UserController.CheckChangePassword(user, "", "password1"), "Able to set from blank");
            Assert.IsTrue(user.HashedPassword.Length > 0, "New Password salt+hash not blank");
            Debug.WriteLine(user.HashedPassword);
        }

        [TestMethod]
        public void TestMethodChangePasswordFromExisting()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                HashedPassword = "1000:lsQrY6Ua0RhOY87r138ykAQnorrHztoP:2zyrQOoEfGxWZJbM/2fhuE9euXPIhG+n"
            };

            Assert.IsTrue(UserController.CheckChangePassword(user, "password1", "password2"), "Able to set from existing");
            Assert.IsTrue(user.HashedPassword.Length > 0, "New Password salt+hash not blank");
            Debug.WriteLine(user.HashedPassword);
        }

        [TestMethod]
        public void TestMethodChangePasswordFromExistingWrong()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                HashedPassword = "1000:lsQrY6Ua0RhOY87r138ykAQnorrHztoP:2zyrQOoEfGxWZJbM/2fhuE9euXPIhG+n"
            };

            Assert.IsFalse(UserController.CheckChangePassword(user, "wrongpassword", "password2"), "Wrong password should be password1");
        }

        
    }
}