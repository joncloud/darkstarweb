using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class AccountRole
    {
        public virtual Account Account { get; set; }
        public long Account_AccountId { get; set; }
        public long AccountRoleId { get; set; }
        public virtual Role Role { get; set; }
        public long Role_RoleId { get; set; }

        internal class Map : EntityTypeConfiguration<AccountRole>
        {
            public Map()
            {
                HasKey(x => x.AccountRoleId);

                HasRequired(x => x.Account).WithMany(x => x.AccountRoles).HasForeignKey(x => x.Account_AccountId);
                HasRequired(x => x.Role).WithMany(x => x.AccountRoles).HasForeignKey(x => x.Role_RoleId).WillCascadeOnDelete();
            }
        }
    }
}