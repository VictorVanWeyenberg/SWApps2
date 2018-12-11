using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Swapps_Web_API.Models;

namespace Swapps_Web_API.Controllers
{
    public class EstablishmentsController : ApiController
    {
        private readonly Swapps_Web_APIContext db = new Swapps_Web_APIContext();

        // GET: api/Establishments
        public IQueryable<Establishment> GetEstablishments()
        {
            return db.Establishments
                .Include(est => est.Address)
                .Include(est => est.Tags)
                .Include(est => est.Events)
                .Include(est => est.Promotions)
                .Include(est => est.ServiceHours);
        }

        // GET: api/Establishments/5
        [ResponseType(typeof(Establishment))]
        public IHttpActionResult GetEstablishment(int id)
        {
            Establishment establishment = db.Establishments.Find(id);
            if (establishment == null)
            {
                return NotFound();
            }

            return Ok(establishment);
        }

        // PUT: api/Establishments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstablishment(int id, Establishment establishment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != establishment.ID)
            {
                return BadRequest();
            }

            db.Entry(establishment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstablishmentExists(id))
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

        // POST: api/Establishments
        [ResponseType(typeof(Establishment))]
        public IHttpActionResult PostEstablishment(Establishment establishment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Establishments.Add(establishment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = establishment.ID }, establishment);
        }

        // DELETE: api/Establishments/5
        [ResponseType(typeof(Establishment))]
        public IHttpActionResult DeleteEstablishment(int id)
        {
            Establishment establishment = db.Establishments.Find(id);
            if (establishment == null)
            {
                return NotFound();
            }

            db.Establishments.Remove(establishment);
            db.SaveChanges();

            return Ok(establishment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstablishmentExists(int id)
        {
            return db.Establishments.Count(e => e.ID == id) > 0;
        }
    }
}