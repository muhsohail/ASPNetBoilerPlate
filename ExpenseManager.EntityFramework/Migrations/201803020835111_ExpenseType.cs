namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseDetails", "ExpenseType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseDetails", "ExpenseType");
        }
    }
}
