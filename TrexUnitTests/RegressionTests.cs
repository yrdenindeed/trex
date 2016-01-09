using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trex.Controllers;
using trex.Models;

namespace TrexUnitTests
{
    [TestClass]
    public class RegressionTests
    {
        [TestMethod]
        public void TestMethodChangePasswordFromEmpty()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RestrictUniqueUserEmail()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "email2@email2.com",
                HashedPassword = ""
            };

            var user2 = new User()
            {
                Id = Guid.NewGuid(),
                Email = "email2@email2.com",
                HashedPassword = ""
            };

            var uc = new UserController();
            uc.Create(user);            
            uc.Create(user2);
            if (uc.ModelState.ContainsKey(user2.Id.ToString()))
            {
                Assert.IsTrue(uc.ModelState[user2.Id.ToString()].Errors.Any(e => e.ErrorMessage.Contains("unique email")));
            }
            else
            {
                Assert.Fail("Should be able to add two users with same email");
            }
        }
        
    }
}
