using apsi.backend.social.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace apsi.backend.social.Models
{
    public class PagingSortDto: PagingDto
    {
        [DefaultValue(false)]
        public bool SortDate { get; set; }

        [DefaultValue(false)]
        public bool SortDescending { get; set; }
    }
}
