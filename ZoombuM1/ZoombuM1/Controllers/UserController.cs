using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZoombuM1.Models;
using ZoombuM1.ServiceReference1;

namespace ZoombuM1.Controllers
{
    public class UserController : Controller
    {

        public async Task<ActionResult> Index(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                int idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                return View(new ZoombuViewModel
                {
                    User = await service.GetUserByIdAsync(idUser),
                    Profil = await service.GetUserByIdAsync(id)
                });
            }
        }

    }
}
