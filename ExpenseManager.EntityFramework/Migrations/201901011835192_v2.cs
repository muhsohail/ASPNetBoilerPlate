namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseCategories", "CategoryType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseCategories", "CategoryType");
        }
    }
}
