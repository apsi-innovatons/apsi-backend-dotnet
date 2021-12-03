using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Models
{
    public class PagingDto
    {
        [DefaultValue(0)]
        public int page { get; set; }
        [DefaultValue(20)]
        public int count { get; set; }
    }
}
