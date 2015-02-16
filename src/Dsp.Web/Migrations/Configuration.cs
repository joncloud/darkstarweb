namespace Dsp.Web.Migrations
{
    using Dsp.Web.Accounting;
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AccountContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
        }

        protected override void Seed(AccountContext context)
        {
            base.Seed(context);

            context.Roles.AddOrUpdate(
                r => r.Name,
                new Role { Name = Role.Administrator, Description = "Can perform administrative operations to the database." });
        }
    }
}
