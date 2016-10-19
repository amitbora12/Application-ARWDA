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
    public class ErrorDatasController : ApiController
    {
        private ARWDADatabaseEntities4 db = new ARWDADatabaseEntities4();

        // GET: api/ErrorDatas
        public IQueryable<ErrorData> GetErrorDatas()
        {
            return db.ErrorDatas;
        }

        // GET: api/ErrorDatas/5
        [ResponseType(typeof(ErrorData))]
        public IHttpActionResult GetErrorData(int id)
        {
            ErrorData errorData = db.ErrorDatas.Find(id);
            if (errorData == null)
            {
                return NotFound();
            }

            return Ok(errorData);
        }

        // PUT: api/ErrorDatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutErrorData(int id, ErrorData errorData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != errorData.ID)
            {
                return BadRequest();
            }

            db.Entry(errorData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ErrorDataExists(id))
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

        // POST: api/ErrorDatas
        [ResponseType(typeof(ErrorData))]
        public IHttpActionResult PostErrorData(ErrorData errorData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ErrorDatas.Add(errorData);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ErrorDataExists(errorData.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = errorData.ID }, errorData);
        }

        // DELETE: api/ErrorDatas/5
        [ResponseType(typeof(ErrorData))]
        public IHttpActionResult DeleteErrorData(int id)
        {
            ErrorData errorData = db.ErrorDatas.Find(id);
            if (errorData == null)
            {
                return NotFound();
            }

            db.ErrorDatas.Remove(errorData);
            db.SaveChanges();

            return Ok(errorData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ErrorDataExists(int id)
        {
            return db.ErrorDatas.Count(e => e.ID == id) > 0;
        }
    }
}