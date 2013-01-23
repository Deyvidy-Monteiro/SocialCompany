using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZoombuM1.Divers;
using ZoombuM1.ServiceReference1;

namespace ZoombuM1.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated == true)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                return View();
            }  
        }

        [HttpPost]
        public async Task<ActionResult> Index(User User)
        {
            using (Service1Client service = new Service1Client())
            {
                if (ModelState.IsValid)
                {
                    // Create MD5 Hash
                    MD5 md5Hash = MD5.Create();
                    string md5Pass = ManageMD5.GetMd5Hash(md5Hash, User.Password);
                    User userExist = await service.GetUserByEmailAndPassAsync(User.Email, md5Pass);

                    if (userExist != null)
                    {
                        FormsAuthentication.SetAuthCookie(userExist.Id.ToString(), false);
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email or password");
                    }
                }
                return View();
            }
        }

    }
}