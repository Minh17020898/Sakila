using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class GroupMessage
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public string DateSent { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
