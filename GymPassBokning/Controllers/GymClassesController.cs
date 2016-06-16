using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymPassBokning.Models;

namespace GymPassBokning.Controllers
{
    [Authorize]
    public class GymClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GymClasses
        [AllowAnonymous]
        public ActionResult Index()
        {
            var date = DateTime.Now;
            var dates = from v in db.Classes
                        select v;
            dates = dates.Where(v => v.StartTime > date);
            //return View(db.Classes.ToList());
            return View(dates.ToList());
        }

        public ActionResult BookedPass()
        {
            var date = DateTime.Now;
            var dates = from v in db.Classes
                        select v;
            
            
            dates = dates.Where(v => v.StartTime > date);
            //GymClass CurrentClass = db.Classes.Where(g => g.GymClassId == g.GymClassId).FirstOrDefault();
            //ApplicationUser CurrentUser = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            
              //  return View(db.Classes.ToList());
           
            //return View(db.Classes.ToList());
           
            return View("BookedPass",dates.ToList());
           
        }
        public ActionResult History()
        {
            
           return View(db.Classes.ToList());
           
        }
        //[Authorize(Roles="admin")]
        // GET: GymClasses/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.Classes.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        //GymClasses/BookingToggle-metod

            public ActionResult BookingToggle(int id)
        {
            GymClass CurrentClass = db.Classes.Where(g => g.GymClassId == id).FirstOrDefault();
            ApplicationUser CurrentUser = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.cUser = CurrentClass.AttendingMembers.Contains(CurrentUser);
            if (CurrentClass.AttendingMembers.Contains(CurrentUser))
            {
                CurrentClass.AttendingMembers.Remove(CurrentUser);
                db.SaveChanges();
            }
            else
            {
                CurrentClass.AttendingMembers.Add(CurrentUser);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        // GET: GymClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GymClassId,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(gymClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gymClass);
        }
        [Authorize(Roles = "admin")]
        // GET: GymClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.Classes.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GymClassId,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gymClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gymClass);
        }
        [Authorize(Roles = "admin")]
        // GET: GymClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.Classes.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }
        [Authorize(Roles = "admin")]
        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GymClass gymClass = db.Classes.Find(id);
            db.Classes.Remove(gymClass);
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
