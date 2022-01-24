using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Models
{
    public class AuthorPagingDto: PagingSortDto
    {
        public string AuthorUsername { get; set; }
    }
}
