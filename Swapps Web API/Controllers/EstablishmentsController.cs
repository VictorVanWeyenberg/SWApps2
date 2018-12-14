using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        [Route("api/establishment/owner")]
        public IHttpActionResult GetEstablishmentForOwner(JObject body)
        {
            if (!body.ContainsKey("Email"))
            {
                return BadRequest("Email required");
            }
            string email = body.Value<string>("Email");
            Entrepreneur entrepreneur = db.Entrepreneurs.Where(e => e.User.Email.Equals(email)).First();
            if (entrepreneur == null)
            {
                return BadRequest("Entrepreneur doesn't exist");
            }
            Establishment establishment = db.Establishments.Where( est => est.ID == entrepreneur.EstablishmentID)
                .Include(est => est.Address)
                .Include(est => est.Tags)
                .Include(est => est.Events)
                .Include(est => est.Promotions)
                .Include(est => est.ServiceHours).First();

            return Ok(establishment);
        }

        [HttpPost]
        [Route("api/establishment/new")]
        public IHttpActionResult PostEstablishment(JObject body)
        {
            if (!CheckRequestBodyForPostEstablishment(body))
            {
                return BadRequest("Body malformed, missing JSON attributes");
            }

            Establishment establishment = new Establishment();
            //Address
            Address address = new Address();
            address.Number = body.Value<int>("Number");
            address.Street = body.Value<string>("Street");
            establishment.Address = address;
            //Type + name
            establishment.Type = (EstablishmentType)Enum.Parse(typeof(EstablishmentType), body.Value<string>("Type"));
            establishment.EstablishmentTypeString = establishment.Type.ToString();
            establishment.Name = body.Value<string>("Name");
            //Tags
            establishment.Tags = ParseTagsForEstablishment(body.Value<JArray>("Tags"));
            establishment.Promotions = new List<Promotion>();
            establishment.Events = new List<EstablishmentEvent>();
            //ServiceHours
            establishment.ServiceHours = ParseServiceHours(body.Value<JArray>("ServiceHours"));

            //Fetch userid for email and set here
            string email = body.Value<string>("Email");
            AbstractUser user = db.AbstractUsers.Where(u => u.Email.Equals(email)).First();
            Entrepreneur entre = db.Entrepreneurs.Where(e => e.UserID == user.ID).First();
            entre.Establishment = establishment;
            db.SaveChanges();

            return Ok();
        }

        private bool CheckRequestBodyForPostEstablishment(JObject body)
        {
            if (!body.ContainsKey("Number")) return false;
            if (!body.ContainsKey("Street")) return false;
            if (!body.ContainsKey("Type")) return false;
            if (!body.ContainsKey("Name")) return false;
            if (!body.ContainsKey("Tags")) return false;
            if (!body.ContainsKey("ServiceHours")) return false;
            if (!body.ContainsKey("Email")) return false;
            return true;
        }

        private List<TimeInterval> ParseServiceHours(JArray array)
        {
            List<TimeInterval> list = new List<TimeInterval>();
            foreach (JObject jsonObject in array)
            {
                TimeInterval time = new TimeInterval();
                time.DayOfWeek = jsonObject.Value<int>("Index");
                if (jsonObject.ContainsKey("StartHour"))
                {
                    time.StartHour = jsonObject.Value<int>("StartHour");
                    time.StartMinute = jsonObject.Value<int>("StartMinute");
                    time.EndHour = jsonObject.Value<int>("EndHour");
                    time.EndMinute = jsonObject.Value<int>("EndMinute");
                }
                list.Add(time);
            }
            return list;
        }

        private List<Tag> ParseTagsForEstablishment(JArray input)
        {
            List<Tag> list = new List<Tag>();
            
            foreach (JObject s in input)
            {
                Tag t = new Tag();
                t.Value = (string)s.GetValue("Value");
                list.Add(t);
            }
            return list;
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