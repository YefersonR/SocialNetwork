using Core.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Comment : AuditableBaseEntity
    {
        public string Content { get; set; }

        public int IdUser { get; set; }
        public User User { get; set; }

        public int IdPost{ get; set; }
        public Post Post { get; set; }

    }
}
