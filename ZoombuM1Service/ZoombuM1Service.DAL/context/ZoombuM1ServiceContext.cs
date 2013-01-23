using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.entities;
using ZoombuM1Service.DAL.mapping;

namespace ZoombuM1Service.DAL.context
{
    public class ZoombuM1ServiceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ZoombuM1ServiceContext() : base("name=ZoombuM1ServiceContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ZoombuM1ServiceContext>());

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User_Mapping());
            modelBuilder.Configurations.Add(new Group_Mapping());
            modelBuilder.Configurations.Add(new Post_Mapping());
            modelBuilder.Configurations.Add(new Like_Mapping());
            modelBuilder.Configurations.Add(new Comment_Mapping());
        }

    }
}
