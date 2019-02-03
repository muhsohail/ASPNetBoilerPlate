namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnusDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnusDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Task = c.String(),
                        Status = c.String(),
                        AssignedTo = c.String(),
                        Progress = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        OnusStatusId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OnusDetails");
        }
    }
}
