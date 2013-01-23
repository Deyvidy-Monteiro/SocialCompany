using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.mapping
{
    public class Post_Mapping : EntityTypeConfiguration<Post>
    {
        public Post_Mapping()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Posts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Picture).HasColumnName("Picture");

            this.HasRequired(s => s.User).WithMany(c => c.Posts).HasForeignKey(s => s.UserId).WillCascadeOnDelete(true);
            this.HasRequired(s => s.Group).WithMany(c => c.Posts).HasForeignKey(s => s.GroupId).WillCascadeOnDelete(true);
        }
    }
}
