using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WaterLevelIndicator.Models;
using WaterLevelIndicator.Models.ViewModels;

namespace WaterLevelIndicator.Controllers
{
    public class LoginController : Controller
    {
        WaterLevelIndicationDBEntities1 db = new WaterLevelIndicationDBEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(WaterLevelIndicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.Database.SqlQuery<int>("EXEC UserLogin @Username, @Password",
                    new SqlParameter("@Username", model.Username),
                    new SqlParameter("@Password", model.Password))
                    .FirstOrDefault();

                if (result == 1)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid username or password" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid model state" });
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
    

