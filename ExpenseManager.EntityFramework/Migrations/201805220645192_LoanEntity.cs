namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoanEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonName = c.String(),
                        LoanAmount = c.Double(nullable: false),
                        AmountReturned = c.Double(nullable: false),
                        ReturnedDate = c.DateTime(),
                        ReturnedByUserId = c.Int(nullable: false),
                        ReturnedByUserName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoanDetails");
        }
    }
}
