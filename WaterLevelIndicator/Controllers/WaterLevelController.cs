using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WaterLevelIndicator.Models;
using WaterLevelIndicator.Models.ViewModels;

namespace WaterLevelIndicator.Controllers
{
    public class WaterLevelController : Controller
    {
        // GET: WaterLevel
        WaterLevelIndicationDBEntities1 db = new WaterLevelIndicationDBEntities1();
        public ActionResult Index()
        {
            var labels = db.Database.SqlQuery<string>("EXEC GetUniqueLabelsFromWaterLevelDataReplica").ToList();
        
                var labelList = new SelectList(labels);
                var viewModel = new WaterLevelViewModel
                {
                    MeasurementDateTime = DateTime.Now,
                    LabelsSelectList = labelList
                };

                return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(WaterLevelViewModel model)
        {
            if (ModelState.IsValid)
            {
                Session["CurrentBox"] = model.Label;
                return Json(new { success = true });
            }

            return View();
        }

        public ActionResult AddNewBox()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewBox(AddBoxforWaterLevelViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("InsertNewStation @Station",
                new SqlParameter("@Station", model.Label));

                return Json(new { success = true });
            }

            return View();
        }
    }
}