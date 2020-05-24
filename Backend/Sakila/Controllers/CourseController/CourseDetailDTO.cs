using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Controllers.CourseController
{
    public class CourseDetailDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public string DateCreated { get; set; }
        public int Participants { get; set; }
        public bool IsJoined { get; set; }
        public int ImageIndex { get; set; }
    }
}
