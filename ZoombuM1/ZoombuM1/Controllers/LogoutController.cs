using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ZoombuM1.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "login");
        }

    }
}
