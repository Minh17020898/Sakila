using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class GroupPermission
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public bool Write { get; set; }
        public bool Authorize { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
