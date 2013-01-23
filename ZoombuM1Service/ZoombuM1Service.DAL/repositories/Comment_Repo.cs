using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.context;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.repositories
{
    public class Comment_Repo
    {
        public static int Create(Comment comment)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    context.Comments.Add(comment);
                    return context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static Comment GetCommentById(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Comments.Include("User").First(p => p.Id == id);
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
                    Comment delete = context.Comments.Find(id);
                    context.Comments.Remove(delete);
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
