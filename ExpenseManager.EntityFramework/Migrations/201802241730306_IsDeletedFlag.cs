namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseDetails", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseDetails", "IsDeleted");
        }
    }
}
