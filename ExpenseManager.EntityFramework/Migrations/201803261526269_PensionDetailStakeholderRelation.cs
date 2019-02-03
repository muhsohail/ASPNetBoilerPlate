namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PensionDetailStakeholderRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StakeholderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Relationship = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PensionDetails", "StakeholderId", c => c.Int());
            CreateIndex("dbo.PensionDetails", "StakeholderId");
            AddForeignKey("dbo.PensionDetails", "StakeholderId", "dbo.StakeholderDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PensionDetails", "StakeholderId", "dbo.StakeholderDetails");
            DropIndex("dbo.PensionDetails", new[] { "StakeholderId" });
            DropColumn("dbo.PensionDetails", "StakeholderId");
            DropTable("dbo.StakeholderDetails");
        }
    }
}
