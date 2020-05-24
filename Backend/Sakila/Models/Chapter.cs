using System;
using System.Collections.Generic;

namespace Sakila.Models
{
    public partial class Chapter
    {
        public Chapter()
        {
            ChapterProgress = new HashSet<ChapterProgress>();
            Question = new HashSet<Question>();
        }

        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<ChapterProgress> ChapterProgress { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
