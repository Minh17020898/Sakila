using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Controllers.ChapterController
{
    public class ProgressDTO
    {
        public Guid ChapterId { get; set; }
        public int Score { get; set; }
    }
}
