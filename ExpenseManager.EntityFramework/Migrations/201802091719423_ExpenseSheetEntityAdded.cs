namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseSheetEntityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Purpose = c.String(),
                        DateSpent = c.DateTime(),
                        Amount = c.Double(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExpenseDetails");
        }
    }
}
