using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class account_session
    {
        public long accid { get; set; }
        public virtual character character { get; set; }
        public long charid { get; set; }
        public long client_addr { get; set; }
        public int client_port { get; set; }
        public long linkshellid { get; set; }
        public int linkshellrank { get; set; }
        public long partyid { get; set; }
        public long server_addr { get; set; }
        public int server_port { get; set; }
        public byte[] session_key { get; set; }
        public int targid { get; set; }

        internal class Map : EntityTypeConfiguration<account_session>
        {
            public Map()
            {
                ToTable("accounts_sessions");

                HasKey(x => new { x.accid, x.charid });

                HasRequired(x => x.character).WithMany(x => x.account_sessions).HasForeignKey(x => x.charid);
            }
        }

    }
}