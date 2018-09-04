namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecialPickup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "SpecialPickup", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "SpecialPickup");
        }
    }
}
