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
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(User user)
        {
            using (Service1Client service = new Service1Client())
            {
                if (ModelState.IsValid)
                {
                    MD5 md5Hash = MD5.Create();
                    string md5Pass = ManageMD5.GetMd5Hash(md5Hash, user.Password);
                    user.Password = md5Pass;
                    
                    if (user.Firstname != null && user.Lastname != null && user.Email != null && user.Password != null && user.Gender != null && user.Department != null && user.Title != null)
                    {
                        User userCreate = await service.CreateAsync(user);
                        if (userCreate != null)
                        {
                            FormsAuthentication.SetAuthCookie(userCreate.Id.ToString(), false);
                            return RedirectToAction("index", "home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid form or user already exist");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "All fields are required.");
                    }
                }
                return View();
            }
        }

    }
}
