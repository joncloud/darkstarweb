namespace Dsp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountActivities",
                c => new
                    {
                        AccountActivityId = c.Long(nullable: false, identity: true),
                        Account_AccountId = c.Long(nullable: false),
                        Description = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        IPAddress = c.String(nullable: false, maxLength: 39, storeType: "nvarchar"),
                        OccurrenceUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.AccountActivityId)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId, cascadeDelete: true)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.AccountRoles",
                c => new
                    {
                        AccountRoleId = c.Long(nullable: false, identity: true),
                        Account_AccountId = c.Long(nullable: false),
                        Role_RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AccountRoleId)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.Account_AccountId)
                .Index(t => t.Role_RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Long(nullable: false, identity: true),
                        Description = c.String(unicode: false),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.AccountActivityChanges",
                c => new
                    {
                        AccountActivityChangeId = c.Long(nullable: false, identity: true),
                        Contents = c.String(nullable: false, unicode: false, storeType: "text"),
                        Key = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Activity_AccountActivityId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AccountActivityChangeId)
                .ForeignKey("dbo.AccountActivities", t => t.Activity_AccountActivityId, cascadeDelete: true)
                .Index(t => t.Activity_AccountActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountActivityChanges", "Activity_AccountActivityId", "dbo.AccountActivities");
            DropForeignKey("dbo.AccountActivities", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.AccountRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.AccountRoles", "Account_AccountId", "dbo.Accounts");
            DropIndex("dbo.AccountActivityChanges", new[] { "Activity_AccountActivityId" });
            DropIndex("dbo.AccountRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.AccountRoles", new[] { "Account_AccountId" });
            DropIndex("dbo.AccountActivities", new[] { "Account_AccountId" });
            DropTable("dbo.AccountActivityChanges");
            DropTable("dbo.Roles");
            DropTable("dbo.AccountRoles");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountActivities");
        }
    }
}
