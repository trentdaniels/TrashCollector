namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNavigation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Employees", "Customer_Id");
            AddForeignKey("dbo.Employees", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Employees", new[] { "Customer_Id" });
            DropColumn("dbo.Employees", "Customer_Id");
        }
    }
}
