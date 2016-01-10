using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using trex.Models;

namespace TrexUnitTests
{
    [TestClass]
    public class IntegrationTests : IntegrationTestsBase
    {

        [TestMethod]
        public void TestMethodSaveTicket()
        {
            var cx = new TrexContext();
            
            var initial = cx.Tickets.Count();
            for (int i = 0; i < 5; i++)
            {
                cx.Tickets.Add(new Ticket()
                {
                    Id = Guid.NewGuid(),
                    Added = DateTime.Now,
                    AssigneeUser = null,
                    Due = DateTime.Now.AddYears(1),
                    Notes = "testingtestingthisbreakxyz12312DHV(UGIWHOIEHF(&*#@^$%^*&(@#",
                    Subject = "test ticket count",
                    Submitter = "bob jones"
                });
            }
            cx.SaveChanges();
            cx.Dispose();

            var cx2 = new TrexContext();
            var after = cx2.Tickets.Count();
            Assert.AreEqual(initial + 5, after, "Not 5 records saved");
            
        }

        /// <summary>
        /// I want to make sure primary key constraints are maintained even if rolling back the transaction during testing (no committ)
        /// I can't see why they wouldn't be, but it could depend on the db provider/dbms. Something to look into more.
        /// In the meantime, this is a nice warning.
        /// </summary>
        [TestMethod]
        public void TestMethodPrimaryKeyConstraint()
        {
            var cx = new TrexContext();

            Guid sameGuid = Guid.NewGuid();
            for (int i = 0; i < 2; i++)
            {
                cx.Tickets.Add(new Ticket()
                {
                    Id = sameGuid,
                    Added = DateTime.Now,
                    AssigneeUser = null,
                    Due = DateTime.Now.AddYears(1),           
                });
            }

            try
            {
                cx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                Assert.IsTrue(true,"All is well, primary key constraint is working");
            }

        

        }

        [TestMethod]
        public void TestSecurable()
        {
            var cx = new TrexContext();
            cx.Tickets.Add(new Ticket()
                {
                    Id = Guid.NewGuid(),
                    Added = DateTime.Now,
                    IsSecure = true,
                    Notes = "{15F022A6-8289-4888-9498-20CC84C80833}"
                });
            cx.Tickets.Add(new Ticket()
            {
                Id = Guid.NewGuid(),
                Added = DateTime.Now,
                IsSecure = false,
                Notes = "{15F022A6-8289-4888-9498-20CC84C80833}"
            });
            cx.SaveChanges();
            var sec = new TrexSecureContext();
            var tickets = sec.Get<Ticket>().Count(u => u.Notes == "{15F022A6-8289-4888-9498-20CC84C80833}");
            Assert.IsTrue(tickets == 1);
        }
    }
}