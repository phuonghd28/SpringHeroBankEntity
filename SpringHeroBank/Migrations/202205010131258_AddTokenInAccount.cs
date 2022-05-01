namespace SpringHeroBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTokenInAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Token", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Token");
        }
    }
}
