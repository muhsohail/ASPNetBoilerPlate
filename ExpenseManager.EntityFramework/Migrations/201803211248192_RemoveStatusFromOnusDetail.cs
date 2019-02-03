namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveStatusFromOnusDetail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OnusDetails", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OnusDetails", "Status", c => c.String());
        }
    }
}
