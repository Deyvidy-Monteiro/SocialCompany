using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoombuM1.ServiceReference1;

namespace ZoombuM1.Models
{
    public class ZoombuViewModel
    {
        public User User { get; set; }
        public User Profil { get; set; }
        public Group Group { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }

        public List<Group> Groups { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<User> Users { get; set; }
    }
}