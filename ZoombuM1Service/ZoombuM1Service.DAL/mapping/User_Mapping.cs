using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.mapping
{
    public class User_Mapping : EntityTypeConfiguration<User>
    {
        public User_Mapping()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Firstname).HasColumnName("Firstname");
            this.Property(t => t.Lastname).HasColumnName("Lastname");
            this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Department).HasColumnName("Department");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Gender).HasColumnName("Gender");

            this.HasMany(c => c.Group).WithMany(p => p.Users).Map(
               m =>
               {
                   m.MapLeftKey("UserId");
                   m.MapRightKey("GroupId");
                   m.ToTable("UserInGroups");
               });

            this.HasMany(c => c.Follow).WithMany(p => p.Users).Map(
               m =>
               {
                   m.MapLeftKey("UserId");
                   m.MapRightKey("UserId2");
                   m.ToTable("Follow");
               });
        }
    }
}
