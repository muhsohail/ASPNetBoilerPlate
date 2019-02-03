namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PensionDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PensionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Reason = c.String(),
                        DateSpent = c.DateTime(),
                        Total = c.Double(nullable: false),
                        TotalSpent = c.Double(nullable: false),
                        Remaining = c.Double(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PensionDetails");
        }
    }
}
