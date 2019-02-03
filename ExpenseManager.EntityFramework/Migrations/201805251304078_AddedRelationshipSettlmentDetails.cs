namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationshipSettlmentDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SettlementDetails", "SettlementTypeId", c => c.Int());
            CreateIndex("dbo.SettlementDetails", "SettlementTypeId");
            AddForeignKey("dbo.SettlementDetails", "SettlementTypeId", "dbo.SettlementCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SettlementDetails", "SettlementTypeId", "dbo.SettlementCategories");
            DropIndex("dbo.SettlementDetails", new[] { "SettlementTypeId" });
            DropColumn("dbo.SettlementDetails", "SettlementTypeId");
        }
    }
}
