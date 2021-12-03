using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apsi.backend.social.Models;

namespace apsi.backend.social.Models
{
    public class PostAnswerDto
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public UserDto Author { get; set; }
    }
}
