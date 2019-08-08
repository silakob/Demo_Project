using Iwanttobuy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Iwanttobuy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //IwanttobuyCustomer user;
            //if (Session["user"] == null)
            //{
            //    user = new IwanttobuyCustomer();
            //}
            //else
            //{
            //    user = Session["user"] as IwanttobuyCustomer;
            //}

            return View();
        }


        public ActionResult Customer()
        {
            return View();
        }

        public ActionResult Order()
        {
            return View();
        }







        #region Util
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion


        #region "User"

        private topicsmartdeviceEntities db = new topicsmartdeviceEntities();


        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            IwanttobuyCustomer iwanttobuyCustomer = new IwanttobuyCustomer();
            iwanttobuyCustomer.Email = form.Get("LoginEmail");
            iwanttobuyCustomer.TextPassword = form.Get("LoginPassword");
            if (String.IsNullOrEmpty(iwanttobuyCustomer.Email) || String.IsNullOrEmpty(iwanttobuyCustomer.TextPassword))
            {
                TempData["Message"] = "Username or Password is wrong";
                TempData["Type"] = "error";

                return View("Index");
            }
            string pwd = iwanttobuyCustomer.TextPassword.ToString();
            iwanttobuyCustomer.Password = Encryption.Encrypt(iwanttobuyCustomer.TextPassword);
            iwanttobuyCustomer = db.IwanttobuyCustomers.Where(m => m.Email == iwanttobuyCustomer.Email && m.Password == iwanttobuyCustomer.Password).FirstOrDefault();

            if (iwanttobuyCustomer == null)
            {
                TempData["Message"] = "Username or Password is wrong";
                TempData["Type"] = "error";

                return View("Index");
            }


            iwanttobuyCustomer.LastLoginDate = DateTime.Now.AddHours(7);
            iwanttobuyCustomer.NoOfLogin = (iwanttobuyCustomer.NoOfLogin == 0 ? 1 : Convert.ToInt32(iwanttobuyCustomer.NoOfLogin) + 1);
            iwanttobuyCustomer.TextPassword = pwd;
            db.Entry(iwanttobuyCustomer).State = EntityState.Modified;
            db.SaveChanges();


            Session["user"] = iwanttobuyCustomer;
            TempData["Message"] = String.Format("ยินดีต้อนรับ คุณ {0} {1}", iwanttobuyCustomer.CustomerName.ToString(), iwanttobuyCustomer.CustomerSurname.ToString());
            TempData["Type"] = "success";
            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                IwanttobuyCustomer iwanttobuyCustomer = new IwanttobuyCustomer();
                iwanttobuyCustomer.CustomerName = form.Get("CustomerName");
                iwanttobuyCustomer.CustomerSurname = form.Get("CustomerSurname");
                iwanttobuyCustomer.Email = form.Get("Email");
                iwanttobuyCustomer.PhoneNo = form.Get("PhoneNo");
                iwanttobuyCustomer.Address = form.Get("Address");
                iwanttobuyCustomer.TextPassword = form.Get("TextPassword");
                byte[] encrypt = Encryption.Encrypt(iwanttobuyCustomer.TextPassword);
                iwanttobuyCustomer.Password = encrypt;
                iwanttobuyCustomer.CreateDate = DateTime.Now.AddHours(7);
                iwanttobuyCustomer.CustomerType = "C";
                iwanttobuyCustomer.NoOfLogin = 0;
                db.IwanttobuyCustomers.Add(iwanttobuyCustomer);
                db.SaveChanges();
                TempData["Message"] = String.Format("คุณ {0} {1} ลงทะเบียนเสร็จสมบูรณ์", iwanttobuyCustomer.CustomerName.ToString(), iwanttobuyCustomer.CustomerSurname.ToString());
                TempData["Type"] = "success";
                return RedirectToAction("Index");
            }

            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session["user"] = null;
            TempData["Message"] = String.Format("คุณออกจากระบบเรียบร้อย");
            TempData["Type"] = "success";
            return View("Index");
        }

        #endregion

    }


}