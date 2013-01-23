using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZoombuM1Service.DAL.entities
{
    [DataContract(IsReference = true)]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public String Firstname { get; set; }

        [DataMember]
        [Required]
        public String Lastname { get; set; }

        [DataMember]
        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        [Required]
        public String Email { get; set; }

        [DataMember]
        [Required]
        public String Password { get; set; }

        [DataMember]
        [Required]
        public String Department { get; set; }

        [DataMember]
        [Required]
        public String Title { get; set; }

        [DataMember]
        [Required]
        public String Gender { get; set; }

        // Relation User -> Group
        [DataMember]
        public virtual ICollection<Group> Group { get; set; }

        // Relation User -> Group Admin
        public virtual ICollection<Group> OwnerGroup { get; set; }

        [DataMember]
        // Relation User -> Posts
        public virtual ICollection<Post> Posts { get; set; }

        // Relation User/Like
        public virtual ICollection<Like> Likes { get; set; }

        // Relation User/Comment
        public virtual ICollection<Comment> Comments { get; set; }

        // Relation User / Follow
        [DataMember]
        public virtual ICollection<User> Follow { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
