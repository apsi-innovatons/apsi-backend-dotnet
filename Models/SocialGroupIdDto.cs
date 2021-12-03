using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apsi.backend.social.Models;
using Microsoft.AspNetCore.Mvc;

namespace apsi.backend.social.Models
{
    [BindProperties]
    public class SocialGroupIdDto: ISocialGroupDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
