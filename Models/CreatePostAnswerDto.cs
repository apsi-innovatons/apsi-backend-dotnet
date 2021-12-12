using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Models
{
    public class CreatePostAnswerDto
    {
        public string Text { get; set; }
        public int PostId { get; set; }
    }
}
