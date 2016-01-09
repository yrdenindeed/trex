using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using trex.Models;
using trex.Utils;
using trex.ViewModels;

namespace trex.Controllers
{
    public class UserController : Controller
    {
        private TrexContext db = new TrexContext();


        // GET: /ChangePassword/
        [Authorize]
        public ActionResult ChangePassword(Guid id)
        {          
            User user = db.Users.Find(id);
           
            return View(new ChangePasswordViewModel(){NewPassword = "", OldPassword = "", User = user});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ViewModels.ChangePasswordViewModel model)
        {
             User user = db.Users.Find(model.UserId);

            if (CheckChangePassword(user,model.OldPassword, model.NewPassword))
            {
                db.SaveChanges();
            }
            
            return View("Index", db.Users.ToList());
        }

        [Authorize]
        public static bool CheckChangePassword(User user, string oldPassword, string newPassword)
        {
            var check = string.IsNullOrEmpty(user.HashedPassword) || PasswordHash.ValidatePassword(oldPassword, user.HashedPassword);
            if (check)
            {
                var newpassword = PasswordHash.CreateHash(newPassword);
                user.HashedPassword = newpassword;
                return true;
            }
            return false;
        }


        // GET: /User/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Type,Email,IsActive,IsAdmin,IsHelpdesk")] SignUpViewModel signUp)
        {
            var user = signUp.User;
            if (db.Users.Any(u => String.Equals(user.Email, u.Email, StringComparison.CurrentCultureIgnoreCase)))
            {
                ModelState.AddModelError(user.Id.ToString(), "Email address already in use, please use a unique email");
            }
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                user.IsActive = true;

                user.HashedPassword = PasswordHash.CreateHash(signUp.NewPassword);

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(signUp);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Type,Email,IsActive,IsAdmin,IsHelpdesk")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /User/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
