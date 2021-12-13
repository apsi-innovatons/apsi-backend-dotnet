using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Models
{
    public class UpdatePostAnswerDto
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
    }
}
