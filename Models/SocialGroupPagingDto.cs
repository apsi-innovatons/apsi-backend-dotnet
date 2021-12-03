using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apsi.backend.social.Models;

namespace apsi.backend.social.Models
{
    public class SocialGroupPagingDto: PagingDto, ISocialGroupDto
    {
        public string Name { get; set; }
    }
}
