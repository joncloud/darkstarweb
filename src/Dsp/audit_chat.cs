using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class audit_chat
    {
        public DateTime datetime { get; set; }
        public long lineID { get; set; }
        public byte[] message { get; set; }
        public string recipient { get; set; }
        public string speaker { get; set; }
        public string type { get; set; }

        internal class Map : EntityTypeConfiguration<audit_chat>
        {
            public Map()
            {
                HasKey(x => x.lineID);
            }
        }
    }
}
