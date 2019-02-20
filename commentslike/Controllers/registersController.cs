using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using commentslike.Models;

namespace commentslike.Controllers
{
    public class registersController : Controller
    {
        private KarthikEntities db = new KarthikEntities();

        // GET: registers
        public ActionResult Index()
        {
            return View(db.registers.ToList());
        }

        // GET: registers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = db.registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: registers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: registers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DateofBirth,Gender,MartialStatus,Emailid,Password,ConfirmPassword,MobileNo")] register register)
        {
            if (ModelState.IsValid)
            {
                db.registers.Add(register);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(register);
        }

        // GET: registers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = db.registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: registers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DateofBirth,Gender,MartialStatus,Emailid,Password,ConfirmPassword,MobileNo")] register register)
        {
            if (ModelState.IsValid)
            {
                db.Entry(register).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(register);
        }

        // GET: registers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            register register = db.registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // POST: registers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            register register = db.registers.Find(id);
            db.registers.Remove(register);
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
        public ActionResult login(register reg, string email, string pass)
        {
             reg.Emailid = email;
             reg.Password = pass;
             var obj = db.registers.ToList();
             var user = obj.SingleOrDefault(usr => usr.Emailid == reg.Emailid && usr.Password == reg.Password);
             if (user != null)
             {
                 Session["email"] = user.Emailid.ToString();
                 Session["password"] = user.Password.ToString();
                 return RedirectToAction("Index", "Home");
             }
            return View();
        }
    }
}
