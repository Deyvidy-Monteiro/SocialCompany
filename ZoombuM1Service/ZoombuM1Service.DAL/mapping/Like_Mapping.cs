using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.mapping
{
    public class Like_Mapping : EntityTypeConfiguration<Like>
    {
        public Like_Mapping()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Like");
            this.Property(t => t.Id).HasColumnName("Id");

            this.HasRequired(s => s.User).WithMany(c => c.Likes).HasForeignKey(s => s.UserId).WillCascadeOnDelete(false);
            this.HasRequired(s => s.Post).WithMany(c => c.Likes).HasForeignKey(s => s.PostId).WillCascadeOnDelete(true);
        }
    }
}
