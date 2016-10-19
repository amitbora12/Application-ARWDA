using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Application_ARWDA.Model;

namespace Application_ARWDA.Controllers
{
    public class AssignPersonController : ApiController
    {
        private ARWDADatabaseEntities4 db = new ARWDADatabaseEntities4();

        // GET: api/AssignPerson
        public IQueryable<AssignPerson> GetAssignPersons()
        {
            return db.AssignPersons;
        }

        // GET: api/AssignPerson/5
        [ResponseType(typeof(AssignPerson))]
        public IHttpActionResult GetAssignPerson(int id)
        {
            AssignPerson assignPerson = db.AssignPersons.Find(id);
            if (assignPerson == null)
            {
                return NotFound();
            }

            return Ok(assignPerson);
        }

        // PUT: api/AssignPerson/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssignPerson(int id, AssignPerson assignPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assignPerson.ID)
            {
                return BadRequest();
            }

            db.Entry(assignPerson).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignPersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AssignPerson
        [ResponseType(typeof(AssignPerson))]
        public IHttpActionResult PostAssignPerson(AssignPerson assignPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssignPersons.Add(assignPerson);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assignPerson.ID }, assignPerson);
        }

        // DELETE: api/AssignPerson/5
        [ResponseType(typeof(AssignPerson))]
        public IHttpActionResult DeleteAssignPerson(int id)
        {
            AssignPerson assignPerson = db.AssignPersons.Find(id);
            if (assignPerson == null)
            {
                return NotFound();
            }

            db.AssignPersons.Remove(assignPerson);
            db.SaveChanges();

            return Ok(assignPerson);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssignPersonExists(int id)
        {
            return db.AssignPersons.Count(e => e.ID == id) > 0;
        }
    }
}