namespace SpringHeroBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewTransaction : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.Transactions", "SenderAccountNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "ReceiverAccountNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Type", c => c.String());
            AlterColumn("dbo.Transactions", "ReceiverAccountNumber", c => c.String());
            AlterColumn("dbo.Transactions", "SenderAccountNumber", c => c.String());
            AlterColumn("dbo.Transactions", "Amount", c => c.String());
        }
    }
}
