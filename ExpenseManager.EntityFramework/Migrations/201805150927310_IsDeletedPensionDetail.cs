namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedPensionDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PensionDetails", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PensionDetails", "IsDeleted");
        }
    }
}
