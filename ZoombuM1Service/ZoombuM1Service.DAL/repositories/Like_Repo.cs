using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.context;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.repositories
{
    public class Like_Repo
    {
        public static int Create(Like like)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    context.Likes.Add(like);
                    return context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static Like GetLikeById(int id)
        {
            try
            {
                using (ZoombuM1ServiceContext context = new ZoombuM1ServiceContext())
                {
                    return context.Likes.Find(id);
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
                    Like delete = context.Likes.Find(id);
                    context.Likes.Remove(delete);
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
