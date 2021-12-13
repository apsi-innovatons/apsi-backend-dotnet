using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Models
{
    public class UpdatePostDto
    {
        public int PostId { get; set; }

        #nullable enable
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? socialGroupName { get; set; }
        #nullable disable

    }
}
