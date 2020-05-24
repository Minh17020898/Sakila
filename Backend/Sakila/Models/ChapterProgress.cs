using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class ChapterProgress
    {
        public Guid UserId { get; set; }
        public Guid ChapterId { get; set; }
        public int Score { get; set; }

        public virtual Chapter Chapter { get; set; }
        public virtual User User { get; set; }
    }
}
