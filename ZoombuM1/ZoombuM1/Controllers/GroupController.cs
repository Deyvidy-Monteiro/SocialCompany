using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZoombuM1.Models;
using ZoombuM1.ServiceReference1;

namespace ZoombuM1.Controllers
{
    public class GroupController : Controller
    {
        public static int groupIdForRedirection;
        public static String previousPage;
        public static int idUser = 0;

        public async Task<ActionResult> Create()
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                return View(new ZoombuViewModel
                {
                    User = await service.GetUserByIdAsync(idUser),
                    Group = new Group()
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(ZoombuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (Service1Client service = new Service1Client())
                {
                    // Get user connected
                    idUser = Convert.ToInt32(HttpContext.User.Identity.Name);

                    Group groupTmp = viewModel.Group;
                    groupTmp.UserOwnerId = idUser;
                    if (await service.CreateGroupAsync(groupTmp) != -1)
                    {
                        //Redirect
                        previousPage = "../Home/";
                        return RedirectToAction(previousPage);
                    }
                    else
                    {
                        return RedirectToAction("Create","Group");
                    }
                 }
            }
            else
            {
                return View(viewModel);
            }
        }

        public async Task<ActionResult> Wall(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                groupIdForRedirection = id;
                Group tmpGroup = await service.GetGroupByIdAsync(id);
                User tmpUser = await service.GetUserByIdAsync(idUser);

                if (tmpGroup.Users.FirstOrDefault(u => u.Id == idUser) != null)
                {
                    return View(new ZoombuViewModel
                    {
                        User = tmpUser,
                        Group = tmpGroup,
                        Post = new Post(),
                        Comment = new Comment(),

                        Groups = tmpUser.Group.ToList(),
                        Posts = await service.GetPostByGroupAsync(tmpGroup),
                        Users = await service.GetAllUserAsync(),
                    });
                }
                else
                {
                    return RedirectToAction("index", "home");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(ZoombuViewModel viewModel, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                using (Service1Client service = new Service1Client())
                {
                    idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                    User tmpUser = await service.GetUserByIdAsync(idUser);
                    Group groupTmp = await service.GetGroupByIdAsync(viewModel.Group.Id);
                    
                    if (tmpUser != null)
                    {
                        Post post = viewModel.Post;
                        post.DateOfCreation = DateTime.Now;

                        Post postCreated = await service.CreatePostAsync(post, idUser, groupTmp.Id);
                        if (postCreated != null && fileUpload != null)
                        {
                            if (fileUpload.ContentType == "image/png")
                            {
                                String fileName = postCreated.Id + ".png";
                                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                                fileUpload.SaveAs(path);
                                await service.AddPictureToAPostAsync(postCreated, "/Images/" + fileName);
                            }
                            if (fileUpload.ContentType == "image/jpeg")
                            {
                                String fileName = postCreated.Id.ToString() + ".jpeg";
                                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                                fileUpload.SaveAs(path);
                                await service.AddPictureToAPostAsync(postCreated, "/Images/" + fileName);
                            }
                        }
                    }
                    Request.UrlReferrer.ToString();
                    previousPage = "Wall/" + groupIdForRedirection;
                    return RedirectToAction(previousPage);
                }
            }
            else
            {
                return View(viewModel);
            }
        }
        
        public async Task<ActionResult> DeletePost(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                Post post = await service.GetPostByIdAsync(id);
                if (post != null)
                {
                    if (post.UserId == idUser)
                    {
                        await service.DeletePostAsync(id);
                    }
                }
                previousPage = "Wall/" + groupIdForRedirection;
                return RedirectToAction(previousPage);
            }
        }

        public async Task<ActionResult> Like(int id)
        {
                using (Service1Client service = new Service1Client())
                {
                    idUser = Convert.ToInt32(HttpContext.User.Identity.Name);

                    if (idUser != 0)
                    {
                        Like like = new Like();
                        like.UserId = idUser;
                        like.PostId = id;
                        await service.CreateLikeAsync(like);
                    }
                    previousPage = "Wall/" + groupIdForRedirection;
                    return RedirectToAction(previousPage);
                } 
        }

        public async Task<ActionResult> UnLike(int Id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                Like like = await service.GetLikeByIdAsync(Id);
                if(like != null){
                    if(like.UserId == idUser ){
                        await service.DeleteLikeAsync(Id);
                    }
                }
                previousPage = "Wall/" + groupIdForRedirection;
                return RedirectToAction(previousPage);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Comment(ZoombuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (Service1Client service = new Service1Client())
                {
                    idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                    User tmpUser = await service.GetUserByIdAsync(idUser);

                    Group groupTmp = await service.GetGroupByIdAsync(viewModel.Group.Id);

                    if (idUser != 0)
                    {
                        Comment comment = viewModel.Comment;
                        if (comment.Commentaire != null)
                        {
                            comment.DateOfCreation = DateTime.Now;
                            comment.UserId = idUser;
                            await service.CreateCommentAsync(comment);
                        }
                    }
                    previousPage = "Wall/" + groupTmp.Id;
                    return RedirectToAction(previousPage);
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        public async Task<ActionResult> DeleteComment(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                Comment comment = await service.GetCommentByIdAsync(id);
                if (comment != null)
                {
                    if (comment.UserId == idUser)
                    {
                        await service.DeleteCommentAsync(id);
                    }
                }
                previousPage = "Wall/" + groupIdForRedirection;
                return RedirectToAction(previousPage);
            }
        }

        public async Task<ActionResult> AddUserToGroup(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                
                if (groupIdForRedirection != 0)
                {
                    Group groupTmp = await service.GetGroupByIdAsync(groupIdForRedirection);
                    if (groupTmp.UserOwnerId == idUser)
                    {
                        await service.AddUserToGroupAsync(id, groupIdForRedirection);
                    }

                }
                previousPage = "Wall/" + groupIdForRedirection;
                return RedirectToAction(previousPage);
            }
        }

        public async Task<ActionResult> RemoveUserFromGroup(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);

                if (groupIdForRedirection != 0)
                {
                    Group groupTmp = await service.GetGroupByIdAsync(groupIdForRedirection);
                    
                    // If owner remove a user
                    if (groupTmp.UserOwnerId == idUser && idUser != id)
                    {
                        await service.RemoveUserFromGroupAsync(id, groupIdForRedirection);
                    }

                    // If user leave group
                    if (groupTmp.UserOwnerId != idUser && idUser == id)
                    {
                        await service.RemoveUserFromGroupAsync(id, groupIdForRedirection);
                    }
                }
                previousPage = "Wall/" + groupIdForRedirection;
                return RedirectToAction(previousPage);
            }
        }

        public async Task<ActionResult> DeleteGroup(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                Group groupTmp = await service.GetGroupByIdAsync(id);
                
                if(groupTmp.UserOwnerId == idUser){
                    await service.DeleteGroupAsync(id);    
                }
                previousPage = "/../Home/";
                return RedirectToAction(previousPage);
            }
        }

        public async Task<ActionResult> Follow(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                Group groupTmp = await service.GetGroupByIdAsync(groupIdForRedirection);

                await service.AddFollowAsync(idUser, id);

                previousPage = "Wall/" + groupIdForRedirection;
                return RedirectToAction(previousPage);
            }
        }

        public async Task<ActionResult> RemoveFollow(int id)
        {
            using (Service1Client service = new Service1Client())
            {
                idUser = Convert.ToInt32(HttpContext.User.Identity.Name);
                Group groupTmp = await service.GetGroupByIdAsync(groupIdForRedirection);

                await service.RemoveFollowAsync(idUser, id);

                String PreviousPage = "Wall/" + groupIdForRedirection;
                return RedirectToAction(PreviousPage);
            }
        }
    }
}