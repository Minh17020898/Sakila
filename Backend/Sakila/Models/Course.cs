using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class Course
    {
        public Course()
        {
            Chapter = new HashSet<Chapter>();
            CoursePermission = new HashSet<CoursePermission>();
            Group = new HashSet<Group>();
        }

        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public string DateCreated { get; set; }
        public string Description { get; set; }
        public int ImageIndex { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<Chapter> Chapter { get; set; }
        public virtual ICollection<CoursePermission> CoursePermission { get; set; }
        public virtual ICollection<Group> Group { get; set; }
    }
}
