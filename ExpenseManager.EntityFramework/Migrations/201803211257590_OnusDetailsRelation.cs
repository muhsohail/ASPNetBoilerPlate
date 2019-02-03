namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnusDetailsRelation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OnusDetails", "OnusStatusId", c => c.Int());
            CreateIndex("dbo.OnusDetails", "OnusStatusId");
            AddForeignKey("dbo.OnusDetails", "OnusStatusId", "dbo.OnusStatus", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnusDetails", "OnusStatusId", "dbo.OnusStatus");
            DropIndex("dbo.OnusDetails", new[] { "OnusStatusId" });
            AlterColumn("dbo.OnusDetails", "OnusStatusId", c => c.Int(nullable: false));
        }
    }
}
