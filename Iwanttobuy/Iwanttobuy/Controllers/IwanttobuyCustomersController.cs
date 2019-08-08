using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Iwanttobuy.Models;

namespace Iwanttobuy.Controllers
{
    public class IwanttobuyCustomersController : Controller
    {
        private topicsmartdeviceEntities db = new topicsmartdeviceEntities();

        // GET: IwanttobuyCustomers
        public ActionResult Index()
        {
            return View(db.IwanttobuyCustomers.ToList());
        }

        // GET: IwanttobuyCustomers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyCustomer iwanttobuyCustomer = db.IwanttobuyCustomers.Find(id);
            if (iwanttobuyCustomer == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyCustomer);
        }

        // GET: IwanttobuyCustomers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IwanttobuyCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerPK,CustomerName,CustomerSurname,Email,Password,CustomerType,PhoneNo,Address,CreateDate,LastLoginDate,NoOfLogin")] IwanttobuyCustomer iwanttobuyCustomer)
        {
            if (ModelState.IsValid)
            {
                db.IwanttobuyCustomers.Add(iwanttobuyCustomer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iwanttobuyCustomer);
        }

        // GET: IwanttobuyCustomers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyCustomer iwanttobuyCustomer = db.IwanttobuyCustomers.Find(id);
            if (iwanttobuyCustomer == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyCustomer);
        }

        // POST: IwanttobuyCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerPK,CustomerName,CustomerSurname,Email,Password,CustomerType,PhoneNo,Address,CreateDate,LastLoginDate,NoOfLogin")] IwanttobuyCustomer iwanttobuyCustomer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iwanttobuyCustomer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iwanttobuyCustomer);
        }

        // GET: IwanttobuyCustomers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyCustomer iwanttobuyCustomer = db.IwanttobuyCustomers.Find(id);
            if (iwanttobuyCustomer == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyCustomer);
        }

        // POST: IwanttobuyCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IwanttobuyCustomer iwanttobuyCustomer = db.IwanttobuyCustomers.Find(id);
            db.IwanttobuyCustomers.Remove(iwanttobuyCustomer);
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
