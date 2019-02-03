namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettelmentCatgeory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SettlementCategories",
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
            DropTable("dbo.SettlementCategories");
        }
    }
}
