using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Controllers.QuestionController
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }

    }
}
