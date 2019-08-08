using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.Collections;
using Newtonsoft.Json;
using SmartSeat.Models;
using OfficeOpenXml;
using System.IO;

namespace SmartWeather.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Main()
        {
            return View();
        }


        public void Alert(string message, Enum.NotificationType notificationType)
        {
            var msg = "swal('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            TempData["notification"] = msg;
        }


        // GET: Home
        public ActionResult Index()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["station"]))
            {
                Session["station"] = Request.QueryString["station"];
            }
            else
            {
                if (Session["station"] == null)
                {
                    Session["station"] = "THDust_001";
                }
            }

            DweetService.JsonDweet dweetObj = new DweetService.JsonDweet();
            dweetObj = DweetService.checkDweetValue();


            return View(dweetObj.with == null ? new DweetService.JsonDweet() : dweetObj);
        }


        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult ExportExcel()
        {
            ExportListUsingEPPlus();
            return View();
        }


        #region HttpPost
        [HttpPost]
        public ActionResult CallAjax()

        {

            DweetService.JsonDweet dweetObj = new DweetService.JsonDweet();
            dweetObj = DweetService.checkDweetValue();

            return Json(dweetObj, JsonRequestBehavior.AllowGet);

        }

        topicsmartdeviceEntities db = new topicsmartdeviceEntities();
        PM_DB globalObject = new PM_DB();

        [HttpPost]
        public ActionResult GetMinMax()
        {
            globalObject.ThingName = Session["station"].ToString();
            Double[] li = new Double[4];
            var dustmin = (from c in db.PM_DB where c.ThingName == globalObject.ThingName select c.Dust).Min();
            var dustmax = (from c in db.PM_DB where c.ThingName == globalObject.ThingName select c.Dust).Max();
            var aqimin = (from c in db.PM_DB where c.ThingName == globalObject.ThingName select c.AQI).Min();
            var aqimax = (from c in db.PM_DB where c.ThingName == globalObject.ThingName select c.AQI).Max();

            if (dustmin == null || dustmax == null || aqimin == null || aqimax == null)
            {
                return null;
            }
            li[0] = Convert.ToDouble(dustmin);
            li[1] = Convert.ToDouble(dustmax);
            li[2] = Convert.ToDouble(aqimin);
            li[3] = Convert.ToDouble(aqimax);
            return Json(li, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Period

        [HttpPost]
        public ActionResult Get3Hour()
        {
            globalObject.ThingName = Session["station"].ToString();
            double validHours = 3;
            var latest = DateTime.Now.AddHours(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Get6Hour()
        {
            globalObject.ThingName = Session["station"].ToString();
            double validHours = 6;
            var latest = DateTime.Now.AddHours(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Get12Hour()
        {
            globalObject.ThingName = Session["station"].ToString();
            double validHours = 12;
            var latest = DateTime.Now.AddHours(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Get1Day()
        {
            globalObject.ThingName = Session["station"].ToString();
            double validHours = 1;
            var latest = DateTime.Now.AddDays(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Get3Day()
        {
            globalObject.ThingName = Session["station"].ToString();
            double validHours = 3;
            var latest = DateTime.Now.AddDays(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Get1Week()
        {
            globalObject.ThingName = Session["station"].ToString();
            double validHours = 7;
            var latest = DateTime.Now.AddDays(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Get2Week()
        {
            globalObject.ThingName = Session["station"].ToString();
            double validHours = 14;
            var latest = DateTime.Now.AddDays(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Get1Month()
        {
            globalObject.ThingName = Session["station"].ToString();
            int validHours = 1;
            var latest = DateTime.Now.AddMonths(-validHours);
            var data = db.PM_DB.Where(s => s.DateTime > latest && s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            if (data == null)
            {
                return null;
            }


            foreach (var group in data)
            {
                group.DateTime = group.DateTime.Value.AddHours(7);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Method
        public void ExportListUsingEPPlus()
        {
            globalObject.ThingName = Session["station"].ToString();
            var data = db.PM_DB.Where(s => s.ThingName == globalObject.ThingName).OrderBy(s => s.DateTime).ToList();


            //data = new[]{
            //                   new{ Name="Ram", Email="ram@techbrij.com", Phone="111-222-3333" },
            //                   new{ Name="Shyam", Email="shyam@techbrij.com", Phone="159-222-1596" },
            //                   new{ Name="Mohan", Email="mohan@techbrij.com", Phone="456-222-4569" },
            //                   new{ Name="Sohan", Email="sohan@techbrij.com", Phone="789-456-3333" },
            //                   new{ Name="Karan", Email="karan@techbrij.com", Phone="111-222-1234" },
            //                   new{ Name="Brij", Email="brij@techbrij.com", Phone="111-222-3333" }
            //          };


            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=Contact.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        #endregion
    }


    public class Enum
    {
        public enum NotificationType
        {
            error,
            success,
            warning,
            info
        }

    }
}