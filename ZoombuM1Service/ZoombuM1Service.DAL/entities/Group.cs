using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ZoombuM1Service.DAL.entities
{
    [DataContract(IsReference=true)]
    public class Group
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String Name { get; set; }

        // Relation Group / User
        [DataMember]
        public ICollection<User> Users { get; set; }

        // Relation Group Admin / User
        [DataMember]
        public int UserOwnerId { get; set; }
        public User User { get; set; }

        // Relation Group / Post
        [DataMember]
        public ICollection<Post> Posts { get; set; }
    }
}
