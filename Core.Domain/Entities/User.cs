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
        public bool IsActiveUser { get; set; } = false;
        public List<Post> Posts { get; set; }
        public List<Comment> Comments{ get; set; }

        public virtual ICollection<Friends> Friend { get; set; }
        
        public virtual ICollection<Friends> FriendOf { get; set; }



    }
}
