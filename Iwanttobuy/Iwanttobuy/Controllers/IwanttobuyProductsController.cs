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
    public class IwanttobuyProductsController : Controller
    {
        private topicsmartdeviceEntities db = new topicsmartdeviceEntities();

        // GET: IwanttobuyProducts
        public ActionResult Index()
        {
            return View(db.IwanttobuyProducts.OrderBy(m => m.Price).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search()
        {
            IwanttobuyProduct iwanttobuyProduct = new IwanttobuyProduct();
            iwanttobuyProduct.ProductName = Request["txtSearch"].ToString();
            if (iwanttobuyProduct.ProductName == "")
            {
                return View("Index", db.IwanttobuyProducts.OrderBy(m => m.Price).ToList());
            }
            ModelState.Clear();
            int value;
            if (int.TryParse(Request["txtSearch"].ToString(), out value))
            {
                iwanttobuyProduct.Price = value;
                return View("Index", db.IwanttobuyProducts.Where(m => m.Price == iwanttobuyProduct.Price).ToList());
            }
            return View("Index", db.IwanttobuyProducts.Where(m => m.ProductName.Contains(iwanttobuyProduct.ProductName) || m.ProductType.Contains(iwanttobuyProduct.ProductName)).ToList());
        }

        // GET: IwanttobuyProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyProduct iwanttobuyProduct = db.IwanttobuyProducts.Find(id);
            if (iwanttobuyProduct == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyProduct);
        }

        // GET: IwanttobuyProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IwanttobuyProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductPK,ProductType,ProductName,Quantity,Price,Description,ImgName,ImgPath,CreateDate,LastUpdate")] IwanttobuyProduct iwanttobuyProduct)
        {
            if (ModelState.IsValid)
            {
                iwanttobuyProduct.CreateDate = DateTime.Now.AddHours(7);
                iwanttobuyProduct.LastUpdate = DateTime.Now.AddHours(7);
                db.IwanttobuyProducts.Add(iwanttobuyProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iwanttobuyProduct);
        }

        // GET: IwanttobuyProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyProduct iwanttobuyProduct = db.IwanttobuyProducts.Find(id);
            if (iwanttobuyProduct == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyProduct);
        }

        // POST: IwanttobuyProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductPK,ProductType,ProductName,Quantity,Price,Description,ImgName,ImgPath,CreateDate,LastUpdate")] IwanttobuyProduct iwanttobuyProduct)
        {
            if (ModelState.IsValid)
            {
                iwanttobuyProduct.LastUpdate = DateTime.Now.AddHours(7);
                db.Entry(iwanttobuyProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iwanttobuyProduct);
        }

        // GET: IwanttobuyProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IwanttobuyProduct iwanttobuyProduct = db.IwanttobuyProducts.Find(id);
            if (iwanttobuyProduct == null)
            {
                return HttpNotFound();
            }
            return View(iwanttobuyProduct);
        }

        // POST: IwanttobuyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IwanttobuyProduct iwanttobuyProduct = db.IwanttobuyProducts.Find(id);
            db.IwanttobuyProducts.Remove(iwanttobuyProduct);
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
