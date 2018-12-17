using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Swapps_Web_API.Models;

namespace Swapps_Web_API.Controllers
{
    public class SubscriptionsController : ApiController
    {
        private Swapps_Web_APIContext db = new Swapps_Web_APIContext();

        // GET: api/Subscriptions
        public IQueryable<Establishment> GetEstablishments()
        {
            return db.Establishments;
        }

        [HttpGet]
        [Route("api/Subscriptions/{userId}")]
        public HttpResponseMessage GetSubscriptionsByUserId(int userId)
        {
            ICollection<Establishment> subscriptions;
            HttpResponseMessage response;
            try
            {
                subscriptions = db.Users
                    .Include(user => user.AbstractUser)
                    .Include(user => user.Subscriptions)
                    .Include(user => user.Subscriptions.Select(est => est.Address))
                    .Include(user => user.Subscriptions.Select(est => est.Tags))
                    .Include(user => user.Subscriptions.Select(est => est.ServiceHours))
                    .Include(user => user.Subscriptions.Select(est => est.Events))
                    .Include(user => user.Subscriptions.Select(est => est.Promotions))
                    .First(user => user.AbstractUser.ID == userId)
                    .Subscriptions;
            } catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.ReasonPhrase = ex.Message + "\n";
                return response;
            }
            response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(subscriptions, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }), Encoding.UTF8, "application/json");
            return response;
        }

        // GET: api/Subscriptions/5
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

        // PUT: api/Subscriptions/5
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

        // POST: api/Subscriptions
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

        // DELETE: api/Subscriptions/5
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