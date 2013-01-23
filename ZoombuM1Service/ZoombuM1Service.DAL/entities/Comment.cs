using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZoombuM1Service.DAL.entities
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public String Commentaire { get; set; }

        [DataMember]
        public DateTime DateOfCreation { get; set; }

        // Relation Comment/Post
        [DataMember]
        public int PostId { get; set; }
        public Post Post { get; set; }

        // Relation Comment/User
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public User User { get; set; }
    }
}
