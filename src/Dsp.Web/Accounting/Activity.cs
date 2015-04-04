using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class Activity
    {
        public string AccountName { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public DateTime OccurrenceUtc { get; set; }
    }
}