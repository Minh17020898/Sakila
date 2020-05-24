using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class Question
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public Guid ChapterId { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }

        public virtual Chapter Chapter { get; set; }
    }
}
