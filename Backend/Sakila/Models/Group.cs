using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupMessage = new HashSet<GroupMessage>();
            GroupPermission = new HashSet<GroupPermission>();
            GroupRequest = new HashSet<GroupRequest>();
        }

        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public string DateCreated { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<GroupMessage> GroupMessage { get; set; }
        public virtual ICollection<GroupPermission> GroupPermission { get; set; }
        public virtual ICollection<GroupRequest> GroupRequest { get; set; }
    }
}
