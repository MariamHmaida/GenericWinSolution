namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskProjectGroups",
                c => new
                    {
                        TaskProject_Id = c.Long(nullable: false),
                        Group_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TaskProject_Id, t.Group_Id })
                .ForeignKey("dbo.TaskProjects", t => t.TaskProject_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.TaskProject_Id)
                .Index(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskProjectGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.TaskProjectGroups", "TaskProject_Id", "dbo.TaskProjects");
            DropIndex("dbo.TaskProjectGroups", new[] { "Group_Id" });
            DropIndex("dbo.TaskProjectGroups", new[] { "TaskProject_Id" });
            DropTable("dbo.TaskProjectGroups");
        }
    }
}
