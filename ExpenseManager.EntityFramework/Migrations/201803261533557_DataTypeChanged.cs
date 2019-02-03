namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StakeholderDetails", "Relationship", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StakeholderDetails", "Relationship", c => c.Boolean(nullable: false));
        }
    }
}
