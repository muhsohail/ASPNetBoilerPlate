namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Settlement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SettlementDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UserName = c.String(),
                        UserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                        ReturnedTo = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SettlementDetails");
        }
    }
}
