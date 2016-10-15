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
using Application_ARWDA.Models;

namespace Application_ARWDA.Controllers
{
    public class LoginsController : ApiController
    {
        private ARDWAEntities db = new ARDWAEntities();

        // GET: api/Logins
        public IQueryable<Login> GetLogins()
        {
            return db.Logins;
        }

        // GET: api/Logins/5
        [ResponseType(typeof(Login))]
        public IHttpActionResult GetLogin(string id)
        {
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // PUT: api/Logins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLogin(string id, Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.Username)
            {
                return BadRequest();
            }

            db.Entry(login).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
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

        // POST: api/Logins
        [ResponseType(typeof(Login))]
        public IHttpActionResult PostLogin(Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Logins.Add(login);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LoginExists(login.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = login.Username }, login);
        }

        // DELETE: api/Logins/5
        [ResponseType(typeof(Login))]
        public IHttpActionResult DeleteLogin(string id)
        {
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            db.Logins.Remove(login);
            db.SaveChanges();

            return Ok(login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginExists(string id)
        {
            return db.Logins.Count(e => e.Username == id) > 0;
        }
    }
}