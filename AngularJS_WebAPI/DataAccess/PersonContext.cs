using AngularJS_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularJS_WebAPI.DataAccess
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public PersonContext() : base("DefaultConnection") {}
    }
}