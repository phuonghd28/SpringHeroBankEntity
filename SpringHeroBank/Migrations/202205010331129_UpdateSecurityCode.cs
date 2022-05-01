namespace SpringHeroBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSecurityCode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "SecurityCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "SecurityCode", c => c.Int(nullable: false));
        }
    }
}
