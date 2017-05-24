namespace App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                        Specialty_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialties", t => t.Specialty_Id)
                .Index(t => t.Specialty_Id);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title_French = c.String(),
                        Title_English = c.String(),
                        Title_Arab = c.String(),
                        Code = c.String(),
                        Description_French = c.String(),
                        Description_English = c.String(),
                        Description_Arab = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name_French = c.String(),
                        Name_English = c.String(),
                        Name_Arab = c.String(),
                        Competence_French = c.String(),
                        Competence_English = c.String(),
                        Competence_Arab = c.String(),
                        Code = c.String(),
                        Presentation_French = c.String(),
                        Presentation_English = c.String(),
                        Presentation_Arab = c.String(),
                        TeachingStrategy_French = c.String(),
                        TeachingStrategy_English = c.String(),
                        TeachingStrategy_Arab = c.String(),
                        Learning_French = c.String(),
                        Learning_English = c.String(),
                        Learning_Arab = c.String(),
                        Evaluation_French = c.String(),
                        Evaluation_English = c.String(),
                        Evaluation_Arab = c.String(),
                        Duration = c.Int(nullable: false),
                        Description_French = c.String(),
                        Description_English = c.String(),
                        Description_Arab = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        State = c.Int(nullable: false),
                        FirstName_French = c.String(),
                        FirstName_English = c.String(),
                        FirstName_Arab = c.String(),
                        LastName_French = c.String(),
                        LastName_English = c.String(),
                        LastName_Arab = c.String(),
                        CIN = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        ProfilePhoto = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        Cellphone = c.String(),
                        FaceBook = c.String(),
                        WebSite = c.String(),
                        Reference = c.String(),
                        Ordre = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        DateModification = c.DateTime(nullable: false),
                        City_Id = c.Long(),
                        Group_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Trainees", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Groups", "Specialty_Id", "dbo.Specialties");
            DropIndex("dbo.Trainees", new[] { "Group_Id" });
            DropIndex("dbo.Trainees", new[] { "City_Id" });
            DropIndex("dbo.Groups", new[] { "Specialty_Id" });
            DropTable("dbo.Trainees");
            DropTable("dbo.Modules");
            DropTable("dbo.Specialties");
            DropTable("dbo.Groups");
        }
    }
}
