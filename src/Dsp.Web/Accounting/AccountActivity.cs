using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class AccountActivity
    {
        public virtual Account Account { get; set; }
        public long Account_AccountId { get; set; }
        public long AccountActivityId { get; set; }
        public virtual ICollection<AccountActivityChange> Changes { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public DateTime OccurrenceUtc { get; set; }

        internal class Map : EntityTypeConfiguration<AccountActivity>
        {
            public Map()
            {
                HasKey(x => x.AccountActivityId);

                Property(x => x.Description).HasMaxLength(128).IsRequired();
                Property(x => x.IPAddress).HasMaxLength(39).IsRequired();

                HasMany(x => x.Changes).WithRequired(x => x.Activity);
                HasRequired(x => x.Account).WithMany(x => x.Activities).HasForeignKey(x => x.Account_AccountId);
            }
        }
    }
}