namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankAccountDetailsEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccountDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        Amount = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BankAccountDetails");
        }
    }
}
