namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ExpenseDetails", "ExpenseCatregoryId", c => c.Int());
            CreateIndex("dbo.ExpenseDetails", "ExpenseCatregoryId");
            AddForeignKey("dbo.ExpenseDetails", "ExpenseCatregoryId", "dbo.ExpenseCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExpenseDetails", "ExpenseCatregoryId", "dbo.ExpenseCategories");
            DropIndex("dbo.ExpenseDetails", new[] { "ExpenseCatregoryId" });
            DropColumn("dbo.ExpenseDetails", "ExpenseCatregoryId");
            DropTable("dbo.ExpenseCategories");
        }
    }
}
