using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class AccountActivityChange
    {
        public long AccountActivityChangeId { get; set; }
        public virtual AccountActivity Activity { get; set; }
        public string Contents { get; set; }
        public string Key { get; set; }

        internal class Map : EntityTypeConfiguration<AccountActivityChange>
        {
            public Map()
            {
                HasKey(x => x.AccountActivityChangeId);

                Property(x => x.Contents).HasColumnType("text").IsRequired();
                Property(x => x.Key).HasMaxLength(128).IsRequired();

                HasRequired(x => x.Activity).WithMany(x => x.Changes);
            }
        }
    }
}