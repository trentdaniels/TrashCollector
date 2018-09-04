namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Employees", new[] { "Customer_Id" });
            AddColumn("dbo.Customers", "Employee_Id", c => c.Int());
            CreateIndex("dbo.Customers", "Employee_Id");
            AddForeignKey("dbo.Customers", "Employee_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Employees", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Customer_Id", c => c.Int());
            DropForeignKey("dbo.Customers", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Customers", new[] { "Employee_Id" });
            DropColumn("dbo.Customers", "Employee_Id");
            CreateIndex("dbo.Employees", "Customer_Id");
            AddForeignKey("dbo.Employees", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
