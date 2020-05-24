using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class GroupRequest
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public bool RequestFromUser { get; set; }
        public string DateRequested { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
