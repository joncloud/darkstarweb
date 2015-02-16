using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class Account
    {
        public long AccountId { get; set; }
        public virtual ICollection<AccountActivity> Activities { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public string UserName { get; set; }

        internal class Map : EntityTypeConfiguration<Account>
        {
            public Map()
            {
                HasKey(x => x.AccountId);

                Property(x => x.AccountId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
                Property(x => x.UserName).HasMaxLength(16).IsRequired();

                HasMany(x => x.Activities).WithRequired(x => x.Account).HasForeignKey(x => x.Account_AccountId);
                HasMany(x => x.AccountRoles).WithRequired(x => x.Account).HasForeignKey(x => x.Account_AccountId);
            }
        }
    }
}