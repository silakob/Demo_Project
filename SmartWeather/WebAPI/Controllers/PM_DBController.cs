using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PM_DBController : ApiController
    {
        private topicsmartdeviceEntities db = new topicsmartdeviceEntities();

        // GET: api/PM_DB
        public IQueryable<PM_DB> GetPM_DB()
        {
            return db.PM_DB;
        }

        // GET: api/PM_DB/5
        [ResponseType(typeof(PM_DB))]
        public IHttpActionResult GetPM_DB(int id)
        {
            PM_DB pM_DB = db.PM_DB.Find(id);
            if (pM_DB == null)
            {
                return NotFound();
            }

            return Ok(pM_DB);
        }

        // PUT: api/PM_DB/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPM_DB(int id, PM_DB pM_DB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pM_DB.PM_PK)
            {
                return BadRequest();
            }

            db.Entry(pM_DB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PM_DBExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PM_DB
        [ResponseType(typeof(PM_DB))]
        public IHttpActionResult PostPM_DB(PM_DB pM_DB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            pM_DB.DateTime = DateTime.Now;
            db.PM_DB.Add(pM_DB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pM_DB.PM_PK }, HttpStatusCode.OK);
        }

        // DELETE: api/PM_DB/5
        [ResponseType(typeof(PM_DB))]
        public IHttpActionResult DeletePM_DB(int id)
        {
            PM_DB pM_DB = db.PM_DB.Find(id);
            if (pM_DB == null)
            {
                return NotFound();
            }

            db.PM_DB.Remove(pM_DB);
            db.SaveChanges();

            return Ok(pM_DB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PM_DBExists(int id)
        {
            return db.PM_DB.Count(e => e.PM_PK == id) > 0;
        }
    }
}