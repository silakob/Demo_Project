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

namespace SmartWeather.Controllers
{
    public class UsersController : Controller
    {
        private topicsmartdeviceEntities db = new topicsmartdeviceEntities();

        public ActionResult Login()
        {
            if (Session["UserKey"] != null)
            {
                return RedirectToAction("Main","Home");
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authorize()
        {
            string UserName = Request.Form["UserName"];
            string Password = Request.Form["Password"];
            if (UserName == "" || Password == "")
            {
                TempData["Message"] = "Username or Password is wrong";
                TempData["Type"] = "error";

                return View("Login");
            }

            User userArg = new User();
            userArg.UserName = UserName;
            userArg.Password = Password;
            User user = db.Users.Where(u => u.UserName == userArg.UserName && u.Password == userArg.Password && u.Active == 1).FirstOrDefault();
            if (user == null)
            {
                TempData["Message"] = "Username or Password is wrong";
                TempData["Type"] = "error";

                return View("Login");
            }


            //return View(user);
            Session["UserKey"] = user.UserKey;
            return RedirectToAction("Main", "Home");
        }


        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }


        // GET: Users/Details/5
        public ActionResult Details(int? id)
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


        // POST: Users/Details/user
        [HttpPost]
        public ActionResult Details(User userArg)
        {
            if (userArg.UserName == null || userArg.Password == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(u => u.UserName == userArg.UserName && u.Password == userArg.Password && u.Active == 1).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            //return View(user);
            return RedirectToAction("LoggedIn");
        }


        public ActionResult LoggedIn()
        {
            if (Session["UserKey"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("User");
            }
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserKey,UserName,Password,Email,PhoneNo,Address,UserType,CreateDate,ModifyDate,NameAndSurname,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                User check = db.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();
                if (check != null)
                {
                    TempData["Message"] = "Username is already exist";
                    TempData["Type"] = "warning";

                    return View("Create");
                }
                user.CreateDate = DateTime.Now;
                user.ModifyDate = DateTime.Now;
                user.Active = 1;
                user.UserType = "C";
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);

        }


        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
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


        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserKey,UserName,Password,Email,PhoneNo,Address,UserType,CreateDate,ModifyDate,NameAndSurname,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
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


        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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


        //public string alert(string msg,string type)
        //{
        //    string str = "swal({ title: 'Smart Label', text: '"+ msg +"', type: '"+ type +"',confirmButtonColor: 'lightskyblue', confirmButtonText: 'OK', closeOnConfirm: false });";
        //    return str;
        //}
    }
}
