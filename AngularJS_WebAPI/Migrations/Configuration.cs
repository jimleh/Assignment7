namespace AngularJS_WebAPI.Migrations
{
    using AngularJS_WebAPI.DataAccess;
    using AngularJS_WebAPI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularJS_WebAPI.DataAccess.PersonContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersonContext context)
        {
            context.Persons.AddOrUpdate(
                p => p.ID,
                new Person { ID = 1, Name = "Jan Jansson", City = "Ockelbo", Occupation = "Kassör" },
                new Person { ID = 2, Name = "Helena Runesdotter", City = "Malmö", Occupation = "Frisör" }
                );
        }
    }
}
