using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.context;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.repositories
{
    public class Group_Repo
    {
       
        public static int Create(Group group)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    if (context.Groups.FirstOrDefault(g => g.Name == group.Name) == null)
                    {
                        context.Groups.Add(group);
                        context.SaveChanges();

                        var UserSearch = context.Users.Include("Group").First(u => u.Id == group.UserOwnerId);
                        var GroupSearch = context.Groups.Include("Users").First(g => g.Id == group.Id);
                        UserSearch.Group.Add(GroupSearch);
                        return context.SaveChanges();
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        public static Group GetGroupById(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Groups.Include("Users").Include("User.Follow").First(s => s.Id == id);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int DeleteGroup(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    Group delete = context.Groups.Find(id);
                    context.Groups.Remove(delete);
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
