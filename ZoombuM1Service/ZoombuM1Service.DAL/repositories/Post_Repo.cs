using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZoombuM1Service.DAL.context;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.repositories
{
    public class Post_Repo
    {
        public static Post Create(Post post, int idUser, int idGroup)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    User userSearch = context.Users.Include("Group").Single(s => s.Id == idUser);
                    Group groupSearch = context.Groups.Include("User").Single(s => s.Id == idGroup);
                    post.Group = groupSearch;
                    post.User = userSearch;

                    context.Posts.Add(post);
                    context.SaveChanges();
                    return post;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int AddPictureToAPost(Post post, String pathPicture)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    Post postEdit = context.Posts.Find(post.Id);                   
                    postEdit.Picture = pathPicture;
                    return context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
       
        public static ICollection<Post> GetPostByGroup(Group group)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Posts.Include("User").Include("Likes").Include("Comments").Include("Comments.User").Where(s => s.GroupId == group.Id).OrderByDescending(p => p.DateOfCreation).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Post GetPostById(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Posts.Include("Likes").First(s => s.Id == id);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ICollection<Post> GetPostFollow(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    User usersTmp = context.Users.Include("Follow").Include("Group").Include("Group.Users").Where(u => u.Id == id).Single();
                    List<Post> posts = new List<Post>();

                    // Foreach user's groups
                    foreach(var item in usersTmp.Group.ToList()){
                        // user's follow
                        foreach (var item2 in usersTmp.Follow)
                        {
                            // If share group
                            if (item2.Group.FirstOrDefault(u => u.Id == item.Id) != null)
                            {
                                List<Post> postTmps = context.Posts.Include("Likes").Where(u => u.UserId == item2.Id && u.GroupId == item.Id).ToList();
                                foreach (var post in postTmps)
                                {
                                    posts.Add(post);
                                }
                            }
                        }                    
                    }
                    return posts.OrderByDescending(p => p.DateOfCreation).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static ICollection<Post> GetPostByName (String name, User user){
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    List<Post> postSearchResult = new List<Post>();

                    foreach (var group in user.Group.ToList())
                    {
                        List <Post> postSearchTmp = context.Posts.Where(p => p.GroupId == group.Id).ToList();
                        foreach (var post in postSearchTmp)
                        {
                            if (post.Content.ToUpper().Contains(name.ToUpper()))
                            {
                                postSearchResult.Add(post);
                            }
                        }
                    }
                    return postSearchResult;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int Delete(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    Post Delete = context.Posts.Include("Likes").First(s => s.Id == id);
                    
                    //Delete picture
                    if (Delete.Picture != null)
                    {
                        try
                        {
                            System.IO.File.Delete(Delete.Picture);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    context.Posts.Remove(Delete);
                    return context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
