using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZoombuM1Service.DAL.entities
{
    [DataContract]
    public class Like
    {
        [DataMember]
        public int Id { get; set; }

        // Relation Like/Post
        [DataMember]
        public int PostId { get; set; }
        public Post Post { get; set; }

        // Relation Like/User
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public User User { get; set; }
    }
}
