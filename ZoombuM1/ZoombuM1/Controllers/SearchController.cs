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
    public class SearchController : Controller
    {

        [HttpPost]
        public async Task<ActionResult> Result()
        {
            using (Service1Client service = new Service1Client())
            {
                // Get input value
                String search = Request["search"].ToString();
                
                int idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                User user = await service.GetUserByIdAsync(idUser);
                
                ICollection<User> resultUserSearch = new HashSet<User>();
                ICollection<Post> resultPostSearch = new HashSet<Post>();
               
                if (search != "")
                {
                    resultUserSearch = await service.GetUserByNameAsync(search);
                    resultPostSearch = await service.GetPostByNameAsync(search, user);
                }

                return View(new ZoombuViewModel
                {
                    User = await service.GetUserByIdAsync(idUser),
                    Users = resultUserSearch,
                    Posts = resultPostSearch,
                });
            }
        }

    }
}
