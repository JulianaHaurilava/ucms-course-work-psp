using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSLib.DTO
{
    public class TemplatedSite
    {
        public required Site Site { get; set; }
        public required Template Template { get; set; }
    }
}
