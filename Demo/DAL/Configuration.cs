namespace App.Migrations
{
    using App.Gwin.Entities.ContactInformations;
    using App.Gwin.Entities.Secrurity.Authentication;
    using GenericWinForm.Demo.DAL;
    using GenericWinForm.Demo.Entities;
    using Gwin;
    using Gwin.Application.BAL;
    using Gwin.Entities.Application;
    using Gwin.Entities.MultiLanguage;
    using Gwin.Entities.Secrurity.Autorizations;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "GwinApp";
        }

        protected override void Seed(ModelContext context)
        {


            // -------------------------------------
            // Giwn App V 0.09
            // -------------------------------------
            // 
            // Gwin Application Name
            //
            context.ApplicationNames.AddOrUpdate(r => r.Reference,
                        new App.Gwin.Entities.Application.ApplicationName
                        {
                            Reference = "Demo",
                            Name = new Gwin.Entities.MultiLanguage.LocalizedString { Arab = "تجريب برنامج الكوين", English = "Gwin Application Demo", French = "Démonstration de l'application Gwin" }
                        }
                      );
            //
            // Gwin Roles
            //
            Role RoleGuest = null;
            Role RoleRoot = null;
            Role RoleAdmin = null;
            context.Roles.AddOrUpdate(
                 r => r.Reference
                        ,
              new Role { Reference = nameof(Role.Roles.Guest), Name = new Gwin.Entities.MultiLanguage.LocalizedString() { Current = nameof(Role.Roles.Guest) } },
              new Role { Reference = nameof(Role.Roles.User), Name = new Gwin.Entities.MultiLanguage.LocalizedString() { Current = nameof(Role.Roles.User) } },
              new Role { Reference = nameof(Role.Roles.Admin), Name = new Gwin.Entities.MultiLanguage.LocalizedString() { Current = nameof(Role.Roles.Admin) } },
              new Role { Reference = nameof(Role.Roles.Root), Name = new Gwin.Entities.MultiLanguage.LocalizedString() { Current = nameof(Role.Roles.Root) }, Hidden = true }
            );
            // Save Change to Select RoleRoot and RoleGuest
            context.SaveChanges();
            RoleRoot = context.Roles.Where(r => r.Reference == nameof(Role.Roles.Root)).SingleOrDefault();
            RoleGuest = context.Roles.Where(r => r.Reference == nameof(Role.Roles.Guest)).SingleOrDefault();
            RoleAdmin = context.Roles.Where(r => r.Reference == nameof(Role.Roles.Admin)).SingleOrDefault();

            // 
            // Giwn Autorizations
            //
            // Guest Autorization
            Authorization FindUserAutorization = new Authorization();
            FindUserAutorization.BusinessEntity = typeof(User).FullName;
            FindUserAutorization.ActionsNames = new List<string>();
            FindUserAutorization.ActionsNames.Add(nameof(IGwinBaseBLO.Recherche));

            RoleGuest.Authorizations = new List<Authorization>();
            RoleGuest.Authorizations.Add(FindUserAutorization);

            // Admin Autorization
            RoleAdmin.Authorizations = new List<Authorization>();

            Authorization UserAutorization = new Authorization();
            UserAutorization.BusinessEntity = typeof(User).FullName;
            RoleAdmin.Authorizations.Add(UserAutorization);


            Authorization CityAutorization = new Authorization();
            CityAutorization.BusinessEntity = typeof(City).FullName;
            RoleAdmin.Authorizations.Add(CityAutorization);
            

            Authorization CountryAutorization = new Authorization();
            CountryAutorization.BusinessEntity = typeof(Country).FullName;
            RoleAdmin.Authorizations.Add(CountryAutorization);
   
            context.SaveChanges();

            //-- Giwn Users
            context.Users.AddOrUpdate(
                u => u.Reference,
                new User() { Reference = nameof(User.Users.Root), Login = nameof(User.Users.Root), Password = nameof(User.Users.Root), LastName = new LocalizedString() { Current = nameof(User.Users.Root) }, Roles = new List<Role>() { RoleRoot } },
                new User() { Reference = nameof(User.Users.Admin), Login = nameof(User.Users.Admin), Password = nameof(User.Users.Admin), LastName = new LocalizedString() { Current = nameof(User.Users.Admin) }, Roles = new List<Role>() { RoleAdmin } },
                new User() { Reference = nameof(User.Users.Guest), Login = nameof(User.Users.Guest), Password = nameof(User.Users.Guest), LastName = new LocalizedString() { Current = nameof(User.Users.Guest) }, Roles = new List<Role>() { RoleGuest } }
                );
            //-- Gwin  Menu
            context.MenuItemApplications.AddOrUpdate(
                            r => r.Code
                         ,
                         new MenuItemApplication { Id = 1, Code = "Configuration", Title = new Gwin.Entities.MultiLanguage.LocalizedString { Arab = "إعدادات", English = "Configuration", French = "Configuration" } },
                         new MenuItemApplication { Id = 2, Code = "Admin", Title = new Gwin.Entities.MultiLanguage.LocalizedString { Arab = "تدبير البرنامج", English = "Admin", French = "Administration" } },
                         new MenuItemApplication { Id = 3, Code = "Root", Title = new Gwin.Entities.MultiLanguage.LocalizedString { Arab = "مصمم اليرنامج", English = "Application Constructor", French = "Rélisateur de l'application" } }
                       );

            //---------------------------------------------------------
            // Sport Club Management System
            //---------------------------------------------------------


            // Autorization
            Authorization ProjectAutorization = new Authorization();
            ProjectAutorization.BusinessEntity = typeof(Project).FullName;
            RoleAdmin.Authorizations.Add(ProjectAutorization);

            Authorization TaskCategoryAutorization = new Authorization();
            TaskCategoryAutorization.BusinessEntity = typeof(TaskCategory).FullName;
            RoleAdmin.Authorizations.Add(TaskCategoryAutorization);

            Authorization TaskProjectAutorization = new Authorization();
            TaskProjectAutorization.BusinessEntity = typeof(TaskCategory).FullName;
            RoleAdmin.Authorizations.Add(TaskProjectAutorization);

            context.SaveChanges();

            // Gwin Test Default Values
            context.EntityMiniConfigs.AddOrUpdate(
                 o => o.Id
              ,
              new TaskProject
              {
                  Id = 1,
                  StartDate = DateTime.Now,
                  Project = new Project() { Id = 1, Title = "Entity_OneToMany" },
                  Categoy = TaskCategory.Analysis,
                  DaysNumber = 3,
                  Title = new Gwin.Entities.MultiLanguage.LocalizedString() { French = "Create Uses Cases Diagrame" },
     
                  Description = new Gwin.Entities.MultiLanguage.LocalizedString() { Arab = "تحليل وظيفي", French = "Create UML Uses Cases Diagrams for Club Management system" },
                  
                  LocalizedTitle = new Gwin.Entities.MultiLanguage.LocalizedString() { Arab = "تحليل وظيفي", French = "Analyse fonctionnelle" },
                  Peoples = new List<Individual>() {
                      new Individual() { LastName = new LocalizedString() {Current = "Mouad" },FirstName = new LocalizedString() {Current = "Madani"} }, 
                      new Individual() { LastName = new LocalizedString() {Current = "Mouad" }  ,FirstName = new LocalizedString() {Current = "Kamal" } }
                  },
                  Responsibles = new List<Individual>() {
                      new Individual() { LastName = new LocalizedString() {Current = "Mouana"} , FirstName =new LocalizedString() {Current = "Chami"}  },
                      new Individual() { LastName = new LocalizedString() {Current = "Kamal"}, FirstName =new LocalizedString() {Current = "Chami"}  }
                  },
              }

            );
        }
    }
}
