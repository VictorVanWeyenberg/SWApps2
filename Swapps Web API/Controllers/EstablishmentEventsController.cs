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
using Swapps_Web_API.Models;

namespace Swapps_Web_API.Controllers
{
    public class EstablishmentEventsController : ApiController
    {
        private Swapps_Web_APIContext db = new Swapps_Web_APIContext();

        // GET: api/EstablishmentEvents
        public IQueryable<EstablishmentEvent> GetEvents()
        {
            return db.Events
                .Include(evt => evt.Establishment)
                .Include(evt => evt.Establishment.Address)
                .Include(evt => evt.Establishment.Events)
                .Include(evt => evt.Establishment.Promotions)
                .Include(evt => evt.Establishment.ServiceHours)
                .Include(evt => evt.Establishment.Tags);
        }

        // GET: api/EstablishmentEvents/5
        [ResponseType(typeof(EstablishmentEvent))]
        public IHttpActionResult GetEstablishmentEvent(int id)
        {
            EstablishmentEvent establishmentEvent = db.Events.Find(id);
            if (establishmentEvent == null)
            {
                return NotFound();
            }

            return Ok(establishmentEvent);
        }

        // PUT: api/EstablishmentEvents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstablishmentEvent(int id, EstablishmentEvent establishmentEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != establishmentEvent.ID)
            {
                return BadRequest();
            }

            db.Entry(establishmentEvent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentEventExists(id))
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

        // POST: api/EstablishmentEvents
        [ResponseType(typeof(EstablishmentEvent))]
        public IHttpActionResult PostEstablishmentEvent(EstablishmentEvent establishmentEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Events.Add(establishmentEvent);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = establishmentEvent.ID }, establishmentEvent);
        }

        // DELETE: api/EstablishmentEvents/5
        [ResponseType(typeof(EstablishmentEvent))]
        public IHttpActionResult DeleteEstablishmentEvent(int id)
        {
            EstablishmentEvent establishmentEvent = db.Events.Find(id);
            if (establishmentEvent == null)
            {
                return NotFound();
            }

            db.Events.Remove(establishmentEvent);
            db.SaveChanges();

            return Ok(establishmentEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstablishmentEventExists(int id)
        {
            return db.Events.Count(e => e.ID == id) > 0;
        }
    }
}