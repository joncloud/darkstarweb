using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dsp.Web.Accounting
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountActivity> AccountActivities { get; set; }
        public DbSet<AccountActivityChange> AccountActivityChanges { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(AccountContext).Assembly);
        }
    }
}