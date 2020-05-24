using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Controllers.CourseController
{
    public class JoinedCourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentScore { get; set; }
        public int TotalScore { get; set; }
        public int ImageIndex { get; set; }
    }
}

