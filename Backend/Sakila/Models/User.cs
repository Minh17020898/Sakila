using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class User
    {
        public User()
        {
            ChapterProgress = new HashSet<ChapterProgress>();
            Course = new HashSet<Course>();
            CoursePermission = new HashSet<CoursePermission>();
            GroupMessage = new HashSet<GroupMessage>();
            GroupPermission = new HashSet<GroupPermission>();
            GroupRequest = new HashSet<GroupRequest>();
            SystemMessage = new HashSet<SystemMessage>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DateCreated { get; set; }

        public virtual ICollection<ChapterProgress> ChapterProgress { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        public virtual ICollection<CoursePermission> CoursePermission { get; set; }
        public virtual ICollection<GroupMessage> GroupMessage { get; set; }
        public virtual ICollection<GroupPermission> GroupPermission { get; set; }
        public virtual ICollection<GroupRequest> GroupRequest { get; set; }
        public virtual ICollection<SystemMessage> SystemMessage { get; set; }
    }
}
