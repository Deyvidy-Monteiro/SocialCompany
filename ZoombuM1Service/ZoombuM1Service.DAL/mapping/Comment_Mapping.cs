using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.mapping
{
    public class Comment_Mapping : EntityTypeConfiguration<Comment>
    {
        public Comment_Mapping()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Comment");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Commentaire).HasColumnName("Commentaire");
            this.Property(t => t.DateOfCreation).HasColumnName("Date");

            this.HasRequired(s => s.User).WithMany(c => c.Comments).HasForeignKey(s => s.UserId).WillCascadeOnDelete(false);
            this.HasRequired(s => s.Post).WithMany(c => c.Comments).HasForeignKey(s => s.PostId).WillCascadeOnDelete(true);
        }
    }
}
