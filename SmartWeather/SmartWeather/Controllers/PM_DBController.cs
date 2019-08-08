using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartWeather;
using System.Data.Entity.Validation;

namespace SmartWeather.Controllers
{
    public class PM_DBController : Controller
    {
        private topicsmartdeviceEntities db = new topicsmartdeviceEntities();

        // GET: PM_DB
        public async Task<ActionResult> Index()
        {
            return View(await db.PM_DB.ToListAsync());
        }

        // GET: PM_DB/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PM_DB pM_DB = await db.PM_DB.FindAsync(id);
            if (pM_DB == null)
            {
                return HttpNotFound();
            }
            return View(pM_DB);
        }

        // GET: PM_DB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PM_DB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PM_PK,ThingName,Time,Dust,AQI,Temp,Hum,Lat,Lon,DateTime")] PM_DB pM_DB)
        {
            if (ModelState.IsValid)
            {
                pM_DB.DateTime = DateTime.Now;
                pM_DB.Time = DateTime.Now.ToString();
                try
                {
                    db.PM_DB.Add(pM_DB);
                    await db.SaveChangesAsync();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            return View(pM_DB);
        }

        // GET: PM_DB/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PM_DB pM_DB = await db.PM_DB.FindAsync(id);
            if (pM_DB == null)
            {
                return HttpNotFound();
            }
            return View(pM_DB);
        }

        // POST: PM_DB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PM_PK,ThingName,Time,Dust,AQI,Temp,Hum,Lat,Lon")] PM_DB pM_DB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pM_DB).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pM_DB);
        }

        // GET: PM_DB/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PM_DB pM_DB = await db.PM_DB.FindAsync(id);
            if (pM_DB == null)
            {
                return HttpNotFound();
            }
            return View(pM_DB);
        }

        // POST: PM_DB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PM_DB pM_DB = await db.PM_DB.FindAsync(id);
            db.PM_DB.Remove(pM_DB);
            await db.SaveChangesAsync();
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




        // POST: PM_DB/Delete/5
        [HttpPost, ActionName("GetPeriodData")]
        public async Task<ActionResult> GetPeriodData(string time)
        {
            if (time == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PM_DB pM_DB = await db.PM_DB.FindAsync(time);
            if (pM_DB == null)
            {
                return HttpNotFound();
            }
            return View(pM_DB);
        }

        [HttpPost]
        public ActionResult GetMinMax()
        {
            Double[] li = { };
            var dustmin = (from c in db.PM_DB select c.Dust).Min();
            var dustmax = (from c in db.PM_DB select c.Dust).Max();
            var aqimin = (from c in db.PM_DB select c.Dust).Min();
            var aqimax = (from c in db.PM_DB select c.Dust).Min();

            if (dustmin == null || dustmax == null || aqimin == null || aqimax == null)
            {
                return null;
            }
            li[0] = Convert.ToDouble(dustmin);
            li[1] = Convert.ToDouble(dustmax);
            li[2] = Convert.ToDouble(aqimin);
            li[3] = Convert.ToDouble(aqimax);
            return Json(li,JsonRequestBehavior.AllowGet);
        }
    }
}
