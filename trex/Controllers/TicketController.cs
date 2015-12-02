using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OpenPop.Mime;
using OpenPop.Pop3;
using trex.Models;

namespace trex.Controllers
{
    public class TicketController : Controller
    {
        private TrexContext db = new TrexContext();

        // GET: /Ticket/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        // GET: /Ticket/
        [Authorize]
        public ActionResult MyNewTickets()
        {
            var user = db.Users.FirstOrDefault(u => u.Id.ToString()== User.Identity.Name.ToString());
            return View("Index", user.Tickets.Where(t => t.FirstOpened == null).ToList());
        }
        
        public static List<Message> FetchUnseenMessages(string hostname, int port, bool useSsl, string username, string password, List<string> seenUids)
        {
            // The client disconnects from the server when being disposed
            using (var client = new Pop3Client())
            {
                client.Connect(hostname, port, useSsl);
                client.Authenticate(username, password);

                // Fetch all the current uids seen
                var uids = client.GetMessageUids();

                // Create a list we can return with all new messages
                var newMessages = new List<Message>();

                // All the new messages not seen by the POP3 client
                for (var i = 0; i < uids.Count; i++)
                {
                    var currentUidOnServer = uids[i];
                    if (seenUids.Contains(currentUidOnServer)) continue;
                    // We have not seen this message before.
                    // Download it and add this new uid to seen uids

                    // the uids list is in messageNumber order - meaning that the first
                    // uid in the list has messageNumber of 1, and the second has 
                    // messageNumber 2. Therefore we can fetch the message using
                    // i + 1 since messageNumber should be in range [1, messageCount]
                    var unseenMessage = client.GetMessage(i + 1);

                    // Add the message to the new messages
                    newMessages.Add(unseenMessage);

                    // Add the uid to the seen uids, as it has now been seen
                    seenUids.Add(currentUidOnServer);
                }

                // Return our new found messages
                return newMessages;
            }
        }


        public const int DueDateDays = 7;
        /// <summary>
        /// use default pop3 web config settings to read mailbox
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ChildActionOnly]
        public ActionResult ReadMailbox()
        {
            ViewData["TicketCount"] = 0;
            var user = db.Users.FirstOrDefault(u => u.Id.ToString() == User.Identity.Name);
            if (user == null) return PartialView("TicketIndicator");
            var company = user.Company;
            // user should get a company when they sign up to trex            
            if (company == null) return PartialView("TicketIndicator");
            var emailIds = db.Tickets.Where(t => !string.IsNullOrEmpty(t.EmailId)).Select(te => te.EmailId).ToList();
            
            var newMail = FetchUnseenMessages(company.MailBoxServer, 
                company.MailboxPort, 
                company.MailboxSsl, 
                company.MailBoxEmailAddress, 
                company.MailBoxPassword,
                emailIds);

            foreach (var message in newMail)
            {
                var customer = db.Customers.FirstOrDefault(cust => cust.Email == message.Headers.From.Address) ??
                                       new Customer {Id = Guid.NewGuid(), Email = message.Headers.From.Address};
                var te = new Ticket
                {
                    Customer = customer,
                    Notes = message.FindFirstPlainTextVersion().GetBodyAsText(),
                    Subject = message.Headers.Subject,
                    Added = DateTime.Now,
                    Due = DateTime.Now.AddDays(DueDateDays),
                    Id = Guid.NewGuid()
                };
                db.Tickets.Add(te);
            }
            db.SaveChanges();

            ViewData["TicketCount"] = newMail.Count;


            return PartialView("TicketIndicator");
        }


        // GET: /Ticket/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: /Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Ticket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Subject,Notes,Added,Due,Submitter")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Id = Guid.NewGuid();
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: /Ticket/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: /Ticket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Subject,Notes,Added,Due,Submitter")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: /Ticket/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: /Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
