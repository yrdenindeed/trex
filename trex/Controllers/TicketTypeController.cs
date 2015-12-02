using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using trex.Models;

namespace trex.Controllers
{
    public class TicketTypeController : Controller
    {
        private TrexContext db = new TrexContext();

        // GET: /TicketType/
        public ActionResult Index()
        {
            return View(db.TicketTypes.ToList());
        }

        // GET: /TicketType/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType tickettype = db.TicketTypes.Find(id);
            if (tickettype == null)
            {
                return HttpNotFound();
            }
            return View(tickettype);
        }

        // GET: /TicketType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TicketType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Type")] TicketType tickettype)
        {
            if (ModelState.IsValid)
            {
                tickettype.Id = Guid.NewGuid();
                db.TicketTypes.Add(tickettype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tickettype);
        }

        // GET: /TicketType/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType tickettype = db.TicketTypes.Find(id);
            if (tickettype == null)
            {
                return HttpNotFound();
            }
            return View(tickettype);
        }

        // POST: /TicketType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Type")] TicketType tickettype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickettype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tickettype);
        }

        // GET: /TicketType/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType tickettype = db.TicketTypes.Find(id);
            if (tickettype == null)
            {
                return HttpNotFound();
            }
            return View(tickettype);
        }

        // POST: /TicketType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TicketType tickettype = db.TicketTypes.Find(id);
            db.TicketTypes.Remove(tickettype);
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
