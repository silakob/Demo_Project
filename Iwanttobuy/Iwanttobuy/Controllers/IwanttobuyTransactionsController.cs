using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Iwanttobuy.Models;

namespace Iwanttobuy.Controllers
{
    public class IwanttobuyTransactionsController : Controller
    {
        private topicsmartdeviceEntities db = new topicsmartdeviceEntities();

        #region Database
        // GET: IwanttobuyTransactions
        public ActionResult Index()
        {
            var sortOrder = new Dictionary<string, int> { { "ยังไม่ได้แพ็คของ", 1 }, { "แพ็คของแล้ว", 2 }, { "ส่งของแล้ว", 3 } };
            var defaultOrder = sortOrder.Max(x => x.Value) + 1;

            return View(db.IwanttobuyTransactions.AsEnumerable().OrderBy(m => sortOrder.TryGetValue(m.ProductState, out var order) ? order : defaultOrder).ThenByDescending(m => m.TransactionID).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search()
        {
            IwanttobuyTransaction iwanttobuyTransaction = new IwanttobuyTransaction();
            iwanttobuyTransaction.CustomerName = Request["txtSearch"].ToString();
            if (iwanttobuyTransaction.CustomerName == "")
            {
                return View("Index", db.IwanttobuyTransactions.OrderByDescending(m => m.TransactionID).ToList());
            }
            ModelState.Clear();
            return View("Index", db.IwanttobuyTransactions.Where(m => m.CustomerName.Contains(iwanttobuyTransaction.CustomerName) || m.Line_IG_Name.Contains(iwanttobuyTransaction.CustomerName)).ToList());
        }

        // GET: IwanttobuyTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyTransaction iwanttobuyTransaction = db.IwanttobuyTransactions.Find(id);
            if (iwanttobuyTransaction == null)
            {
                return HttpNotFound();
            }
            iwanttobuyTransaction.Datetobuy = new DateTime(iwanttobuyTransaction.Datetobuy.Year, iwanttobuyTransaction.Datetobuy.Month, iwanttobuyTransaction.Datetobuy.Day,
                iwanttobuyTransaction.Datetobuy.Hour, iwanttobuyTransaction.Datetobuy.Minute, 0, 0);
            return View(iwanttobuyTransaction);
        }

        // GET: IwanttobuyTransactions/Create
        public ActionResult Create()
        {
            IwanttobuyTransaction iwanttobuyObj = new IwanttobuyTransaction();
            iwanttobuyObj.Datetobuy = DateTime.Now.AddHours(7);
            iwanttobuyObj.Earring = 0;
            iwanttobuyObj.Bracelet = 0;
            iwanttobuyObj.Hairpin = 0;
            iwanttobuyObj.Ring = 0;
            iwanttobuyObj.Necklace = 0;
            return View(iwanttobuyObj);
        }

        // POST: IwanttobuyTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,CustomerName,Line_IG_Name,Necklace,Bracelet,Earring,Hairpin,Ring,Address,Datetobuy,TotalPrice,ProductState,Remark")] IwanttobuyTransaction iwanttobuyTransaction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string dString = Request["Datetobuy"].ToString();
                    iwanttobuyTransaction.Datetobuy = DateTime.Parse(dString);
                    //iwanttobuyTransaction.Datetobuy = DateTime.Parse(dString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    //iwanttobuyTransaction.Datetobuy = DateTime.Now.AddHours(7);
                    db.IwanttobuyTransactions.Add(iwanttobuyTransaction);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["Message"] = e.Message;
                    if (e.Message == "An error occurred while updating the entries. See the inner exception for details.")
                    {
                        TempData["Message"] += " : กรอกวันที่ผิดรูปแบบ ตัวอย่าง dd/MM/yyyy HH:mm:ss";
                    }
                    TempData["Type"] = "error";
                    //throw;
                }                
            }

            return View(iwanttobuyTransaction);
        }

        // GET: IwanttobuyTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyTransaction iwanttobuyTransaction = db.IwanttobuyTransactions.Find(id);
            if (iwanttobuyTransaction == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyTransaction);
        }

        // POST: IwanttobuyTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,CustomerName,Line_IG_Name,Necklace,Bracelet,Earring,Hairpin,Ring,Address,Datetobuy,TotalPrice,ProductState,Remark")] IwanttobuyTransaction iwanttobuyTransaction)
        {
            if (ModelState.IsValid)
            {
                string dString = Request["Datetobuy"].ToString();
                DateTime d = DateTime.ParseExact(dString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                iwanttobuyTransaction.Datetobuy = d;
                db.Entry(iwanttobuyTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iwanttobuyTransaction);
        }

        // GET: IwanttobuyTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyTransaction iwanttobuyTransaction = db.IwanttobuyTransactions.Find(id);
            if (iwanttobuyTransaction == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyTransaction);
        }

        // POST: IwanttobuyTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IwanttobuyTransaction iwanttobuyTransaction = db.IwanttobuyTransactions.Find(id);
            db.IwanttobuyTransactions.Remove(iwanttobuyTransaction);
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

        #endregion


        #region Report
        public ActionResult Report()
        {
            return View();
        }
        #endregion
    }
}
