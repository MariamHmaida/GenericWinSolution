namespace GenericWinForm.Demo.DAL
{
    using App.DAL;
    using App.Gwin.Entities.Application;
    using App.Gwin.Entities.ContactInformations;
    using App.Gwin.Entities.Logging;
    using App.Gwin.Entities.Secrurity.Authentication;
    using App.Gwin.Entities.Secrurity.Autorizations;
    using App.Gwin.GwinApplication.BLL.Configuration;
    using Entities.ProjectManager;
    using Entities.TraineeManagement;
    using Entities.TrainingManagement;
    using GenericWinForm.Demo.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ModelContext : DbContext
    {

        //public ModelContext() : base(@"data source =.\SQLEXPRESS; initial catalog = gwin-demo2; user = sa; password = admintp4; MultipleActiveResultSets = True; App = EntityFramework")
        //{

        //}

        //public ModelContext()
        //  : base("name=GwinDB")
        //{
        //}

        public ModelContext() : base(new ConfigDbBLO().getConnectionString())
        {

        }

        //public ModelContext() : base(LocalDB.GetLocalDBConnectionString("GwinDemo"))
        //{
        //    // Database.SetInitializer<ModelContext>(new CreateDatabaseIfNotExists<ModelContext>());

        //}

        //
        // Gwin : Entites
        //
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Authorization> Authorizations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<GwinActivity> GwinActivities { get; set; }
        public virtual DbSet<MenuItemApplication> MenuItemApplications { get; set; }
        public virtual DbSet<Country> Countrys { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
        public virtual DbSet<ApplicationName> ApplicationNames { get; set; }


        // Demo
        public virtual DbSet<TaskProject> TaskProjects { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        // Trainne
        public virtual DbSet<Specialty> Specialtys { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }
        public virtual DbSet<Module> Modules { get; set; }

        



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskProject>()
                .HasMany<Individual>(s => s.Peoples)
                .WithMany(c => c.Histasks);

            modelBuilder.Entity<TaskProject>()
                .HasMany<Individual>(s => s.Responsibles)
                .WithMany(c => c.ResponsibilityFortasks);

        }

        /// <summary>
        /// trouver la liste des type des objets dans le context
        /// </summary>
        /// <returns></returns>
        public List<Type> GetTypesSets()
        {
            var sets = from p in typeof(ModelContext).GetProperties() where p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) let entityType = p.PropertyType.GetGenericArguments().First() select p.PropertyType.GetGenericArguments()[0];
            return sets.ToList<Type>();
        }

    }

    


}