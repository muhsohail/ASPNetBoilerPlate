namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseDetails", "UserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseDetails", "UserId");
        }
    }
}
