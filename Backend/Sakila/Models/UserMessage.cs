using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class UserMessage
    {
        public Guid Id { get; set; }
        public Guid FollowerId { get; set; }
        public string Content { get; set; }
        public byte[] DateSent { get; set; }
    }
}
