namespace Dsp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CacheAccountUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "UserName", c => c.String(nullable: false, maxLength: 16, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "UserName");
        }
    }
}
