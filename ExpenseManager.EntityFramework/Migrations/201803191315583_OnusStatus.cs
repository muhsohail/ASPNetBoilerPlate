namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnusStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnusStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OnusStatus");
        }
    }
}
