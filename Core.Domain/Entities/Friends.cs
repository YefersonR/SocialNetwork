using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Friends
    {
        public int Id{ get; set; }

        public int IdUser { get; set; }

        public User User { get; set; }
        public int IdFriend {get; set;} 
        public User Friend { get; set; }
    }
}
