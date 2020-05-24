using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class CoursePermission
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public bool Write { get; set; }
        public bool Authorize { get; set; }
        public bool Join { get; set; }

        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
