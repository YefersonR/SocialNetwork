using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName{ get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public string  Mail { get; set; }
        public string  UserName { get; set; }
        public string Password { get; set; }
        /*
        public ICollection<Post> Posts { get; set; }
        public virtual ICollection<User> Friend { get; set; }
        public virtual ICollection<User> FriendOf { get; set; }

        public ICollection<Comment> Comments{ get; set; }
        */

    }
}
