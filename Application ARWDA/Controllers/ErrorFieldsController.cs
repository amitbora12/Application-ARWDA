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
    public class ErrorFieldsController : ApiController
    {
        private ARWDADatabaseEntities4 db = new ARWDADatabaseEntities4();

        // GET: api/ErrorFields
        public IQueryable<ErrorField> GetErrorFields()
        {
            return db.ErrorFields;
        }

        // GET: api/ErrorFields/5
        [ResponseType(typeof(ErrorField))]
        public IHttpActionResult GetErrorField(string id)
        {
            ErrorField errorField = db.ErrorFields.Find(id);
            if (errorField == null)
            {
                return NotFound();
            }

            return Ok(errorField);
        }

        // PUT: api/ErrorFields/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutErrorField(string id, ErrorField errorField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != errorField.ErrorData)
            {
                return BadRequest();
            }

            db.Entry(errorField).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorFieldExists(id))
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

        // POST: api/ErrorFields
        [ResponseType(typeof(ErrorField))]
        public IHttpActionResult PostErrorField(ErrorField errorField)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ErrorFields.Add(errorField);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ErrorFieldExists(errorField.ErrorData))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = errorField.ErrorData }, errorField);
        }

        // DELETE: api/ErrorFields/5
        [ResponseType(typeof(ErrorField))]
        public IHttpActionResult DeleteErrorField(string id)
        {
            ErrorField errorField = db.ErrorFields.Find(id);
            if (errorField == null)
            {
                return NotFound();
            }

            db.ErrorFields.Remove(errorField);
            db.SaveChanges();

            return Ok(errorField);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ErrorFieldExists(string id)
        {
            return db.ErrorFields.Count(e => e.ErrorData == id) > 0;
        }
    }
}