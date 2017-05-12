namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskProjects", "Title_French", c => c.String());
            AddColumn("dbo.TaskProjects", "Title_English", c => c.String());
            AddColumn("dbo.TaskProjects", "Title_Arab", c => c.String());
            AddColumn("dbo.TaskProjects", "Description_French", c => c.String());
            AddColumn("dbo.TaskProjects", "Description_English", c => c.String());
            AddColumn("dbo.TaskProjects", "Description_Arab", c => c.String());
            DropColumn("dbo.TaskProjects", "Title");
            DropColumn("dbo.TaskProjects", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskProjects", "Description", c => c.String());
            AddColumn("dbo.TaskProjects", "Title", c => c.String());
            DropColumn("dbo.TaskProjects", "Description_Arab");
            DropColumn("dbo.TaskProjects", "Description_English");
            DropColumn("dbo.TaskProjects", "Description_French");
            DropColumn("dbo.TaskProjects", "Title_Arab");
            DropColumn("dbo.TaskProjects", "Title_English");
            DropColumn("dbo.TaskProjects", "Title_French");
        }
    }
}
