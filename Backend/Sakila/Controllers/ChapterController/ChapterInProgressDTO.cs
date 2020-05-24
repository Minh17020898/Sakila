using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Controllers.ChapterController
{
    public class ChapterInProgressDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentScore { get; set; }
        public int TotalScore { get; set; }
    }
}
