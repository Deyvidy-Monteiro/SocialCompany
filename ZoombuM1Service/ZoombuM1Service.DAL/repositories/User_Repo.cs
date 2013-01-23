using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.context;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.repositories
{
    public class User_Repo
    {
        public static User Create(User user)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    // check user already exist
                    user.Group = null;
                    user.Likes = null;
                    user.Posts = null;
                    if (context.Users.Where(s => s.Email == user.Email).FirstOrDefault() == null)
                    {
                        // create user
                        context.Users.Add(user);

                        // assign group to user
                        string email = user.Email;
                        string[] split = email.Split('@');
                        string nomDomaine = split[1].ToString();
                        split = nomDomaine.Split('.');
                        string nomGroupe = split[0].ToString();

                        // check group already exist
                        Group existGroup = context.Groups.Where(s => s.Name == nomGroupe).FirstOrDefault();

                        if (existGroup == null)
                        {
                            Group groupForUser = new Group();
                            groupForUser.Name = nomGroupe;
                            groupForUser.UserOwnerId = user.Id;
                            context.Groups.Add(groupForUser);
                            context.SaveChanges();
                            User_Repo.AddUserToGroup(user.Id, groupForUser.Id);
                        }
                        else
                        {
                            context.SaveChanges();
                            User_Repo.AddUserToGroup(user.Id, existGroup.Id);
                        }

                        context.SaveChanges();
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static User GetUserByEmailAndPass(String email, String pass)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Users.Where(s => s.Email == email && s.Password == pass).First();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static User GetUserById(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Users.Include("Group").Include("Follow").Where(s => s.Id == id).First();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<User> GetUserByName(String name)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    List<User> userSearchResult = new List<User>();
                    foreach (var user in context.Users.ToList())
                    {
                        String nameUser = user.Firstname + " " + user.Lastname;
                        if (nameUser.ToUpper().Contains(name.ToUpper()))
                        {
                            userSearchResult.Add(user);
                        }
                    }
                    return userSearchResult;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ICollection<User> GetAllUser()
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Users.Include("Group").ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int AddFollow(int idUser, int idUserDestinataire)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    User userDestinataire = context.Users.Include("Users").Where(s => s.Id == idUserDestinataire).First();
                    User user = context.Users.Include("Follow").Where(s => s.Id == idUser).First();
                    user.Follow.Add(userDestinataire);
                    return context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int RemoveFollow(int idUser, int IdUserDestinataire)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    User userDestinataire = context.Users.Include("Users").Where(s => s.Id == IdUserDestinataire).First();
                    User user = context.Users.Include("Follow").Where(s => s.Id == idUser).First();
                    user.Follow.Remove(userDestinataire);
                    return context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int AddUserToGroup(int idUser, int idGroup)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    User userSearch = context.Users.Include("Group").First(s => s.Id == idUser);
                    Group groupSearch = context.Groups.Include("Users").First(s => s.Id == idGroup);

                    userSearch.Group.Add(groupSearch);

                    return context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int RemoveUserFromGroup(int idUser, int idGroup)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    User userSearch = context.Users.Include("Group").First(s => s.Id == idUser);
                    Group groupSearch = context.Groups.Include("Users").First(s => s.Id == idGroup);

                    userSearch.Group.Remove(groupSearch);

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
