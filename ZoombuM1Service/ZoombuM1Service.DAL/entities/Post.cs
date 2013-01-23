using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZoombuM1Service.DAL.entities
{
    [DataContract(IsReference = true)]
    public class Post
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public String Content { get; set; }
        
        [DataMember]
        public String Picture { get; set; }

        [DataMember]
        public DateTime DateOfCreation { get; set; }

        // Relation Post/User
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public User User { get; set; }

         // Relation Post/Group
        [DataMember]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        // Relation Post/Like
        [DataMember]
        public virtual ICollection<Like> Likes { get; set; }

        // Relation Post/Comment
        [DataMember]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
