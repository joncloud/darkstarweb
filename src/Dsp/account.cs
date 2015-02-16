using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dsp
{
    public class account
    {
        public virtual ICollection<character> characters { get; set; }
        public long content_ids { get; set; }
        public string email { get; set; }
        public string email2 { get; set; }
        public long firstip { get; set; }
        public long id { get; set; }
        public long lastip { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public long priv { get; set; }
        public long status { get; set; }
        public DateTime timecreate { get; set; }
        public DateTime timelastmodify { get; set; }

        internal class Map : EntityTypeConfiguration<account>
        {
            public Map()
            {
                ToTable("accounts");

                HasKey(x => x.id);

                HasMany(x => x.characters).WithRequired(x => x.account).HasForeignKey(x => x.accid);
            }
        }
    }
}
