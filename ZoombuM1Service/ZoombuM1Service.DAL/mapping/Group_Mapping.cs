using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service.DAL.mapping
{
    public class Group_Mapping : EntityTypeConfiguration<Group>
    {
        public Group_Mapping()
        {
            this.HasKey(t => t.Id);
            this.ToTable("Groups");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");

            this.HasRequired(s => s.User).WithMany(c => c.OwnerGroup).HasForeignKey(s => s.UserOwnerId).WillCascadeOnDelete(false);
        }
    }
}
