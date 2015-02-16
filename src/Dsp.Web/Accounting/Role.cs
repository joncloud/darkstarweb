using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    public class Role
    {
        public const string Administrator = "Administrator";

        public string Description { get; set; }
        public string Name { get; set; }
        public long RoleId { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }

        internal class Map : EntityTypeConfiguration<Role>
        {
            public Map()
            {
                HasKey(x => x.RoleId);

                HasMany(x => x.AccountRoles).WithRequired(x => x.Role).HasForeignKey(x => x.Role_RoleId).WillCascadeOnDelete();
            }
        }
    }
}