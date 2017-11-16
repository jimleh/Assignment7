using AngularJS_WebAPI.Models;
using AngularJS_WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AngularJS_WebAPI.Controllers
{
    public class PersonController : ApiController
    {
        PersonRepository repo;

        public PersonController()
        {
            repo = new PersonRepository();
        }

        [ResponseType(typeof(Person))]
        public IHttpActionResult Get()
        {
            return Ok(repo.GetAllPersons());
        }

        [ResponseType(typeof(Person))]
        public IHttpActionResult Get(int? id)
        {
            if(id == null)
            {
                return BadRequest("Id cannot be null!");
            }

            var person = repo.GetPerson(id.Value);
            if(person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [ResponseType(typeof(Person))]
        public IHttpActionResult Post([FromBody]Person person)
        {
            if(ModelState.IsValid)
            {
                repo.AddPerson(person);
                return Ok("Person added to PersonDB!");
            }

            return BadRequest("Something went horribly wrong!");
        }

        [ResponseType(typeof(Person))]
        public IHttpActionResult Put(int? id, [FromBody]Person newPerson)
        {
            if(id == null || id != newPerson.ID)
            {
                return BadRequest("Id cannot be null!");
            }

            var person = repo.GetPerson(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            repo.EditPerson(person, newPerson);

            return Ok("Person Updated!");
        }
        
        [ResponseType(typeof(Person))]
        public IHttpActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var person = repo.GetPerson(id.Value);
            if(person == null)
            {
                return NotFound();
            }
            repo.RemovePerson(person);
            return Ok("Person Deleted Successfully!");

        }
    }
}
