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
    public class AuditController : ApiController
    {
        private ARWDADatabaseEntities4 db = new ARWDADatabaseEntities4();

        // GET: api/Audit
        public IQueryable<Audit> GetAudits()
        {
            return db.Audits;
        }

        // GET: api/Audit/5
        [ResponseType(typeof(Audit))]
        public IHttpActionResult GetAudit(string id)
        {
            Audit audit = db.Audits.Find(id);
            if (audit == null)
            {
                return NotFound();
            }

            return Ok(audit);
        }

        // PUT: api/Audit/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAudit(string id, Audit audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != audit.ProcesseedFileName)
            {
                return BadRequest();
            }

            db.Entry(audit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditExists(id))
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

        // POST: api/Audit
        [ResponseType(typeof(Audit))]
        public IHttpActionResult PostAudit(Audit audit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Audits.Add(audit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AuditExists(audit.ProcesseedFileName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = audit.ProcesseedFileName }, audit);
        }

        // DELETE: api/Audit/5
        [ResponseType(typeof(Audit))]
        public IHttpActionResult DeleteAudit(string id)
        {
            Audit audit = db.Audits.Find(id);
            if (audit == null)
            {
                return NotFound();
            }

            db.Audits.Remove(audit);
            db.SaveChanges();

            return Ok(audit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuditExists(string id)
        {
            return db.Audits.Count(e => e.ProcesseedFileName == id) > 0;
        }
    }
}