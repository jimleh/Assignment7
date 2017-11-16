using AngularJS_WebAPI.DataAccess;
using AngularJS_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularJS_WebAPI.Repositories
{
    public class PersonRepository
    {
        PersonContext context;
        public PersonRepository()
        {
            context = new PersonContext();
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return context.Persons;
        }

        public Person GetPerson(int id)
        {
            return context.Persons.FirstOrDefault(p => p.ID == id);
        }

        public void AddPerson(Person person)
        {
            context.Persons.Add(person);
            context.SaveChanges();
        }
        public void RemovePerson(Person person)
        {
            context.Persons.Remove(person);
            context.SaveChanges();
        }
        public void EditPerson(Person person, Person newPerson)
        {
            // This might be entirely unnecessary, but I just wanted to try doing this with reflection
            foreach(var prop in person.GetType().GetProperties())
            {
                prop.SetValue(person, prop.GetValue(newPerson));
            }
            context.Entry(person).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}