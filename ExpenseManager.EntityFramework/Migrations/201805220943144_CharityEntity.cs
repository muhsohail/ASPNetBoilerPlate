namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharityEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharityDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Purpose = c.String(),
                        DateSpent = c.DateTime(),
                        Amount = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        UserName = c.String(),
                        UserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CharityDetails");
        }
    }
}
