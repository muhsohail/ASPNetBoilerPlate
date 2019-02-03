namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPensionReceivableEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PensionReceivableDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PensionReceivableDetails");
        }
    }
}
