namespace MilanMilicIspit.Migrations
{
    using MilanMilicIspit.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MilanMilicIspit.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MilanMilicIspit.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Galeries.AddOrUpdate(x => x.Id,
           new Gallery() { Id = 1, Name = "Prva galerija", YearOfEstablishment = 2010 },
           new Gallery() { Id = 2, Name = "Art galerija", YearOfEstablishment = 2012 },
           new Gallery() { Id = 3, Name = "Vivo galerija", YearOfEstablishment = 1988 }

           );
            context.SaveChanges();

            context.Pictures.AddOrUpdate(x => x.Id,
              new Picture() { Id = 1, Name = "Prolece", Author= "Aleksandar Kumric", Year = 1969, Price =380, GaleryId = 2 },
              new Picture() { Id = 2, Name = "Drvo", Author = "Marko Petrovic", Year = 2010, Price = 2100, GaleryId = 3 },
              new Picture() { Id = 3, Name = "Tihovanje", Author = "Ljubodrag Jankovic", Year = 2005, Price = 1200, GaleryId = 1 },
              new Picture() { Id = 4, Name = "Kanjon Morace", Author = "Vojo Dimitrijevic", Year = 1980, Price = 220, GaleryId = 2 },
              new Picture() { Id = 5, Name = "Trg", Author = "Zorka Cerovic", Year = 1947, Price = 400, GaleryId = 1 }

              );
            context.SaveChanges();
        }
    }
}
