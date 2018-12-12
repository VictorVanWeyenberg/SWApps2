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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Swapps_Web_API.Controllers
{
    public class AbstractUsersController : ApiController
    {
        private readonly Swapps_Web_APIContext db = new Swapps_Web_APIContext();

        /// <summary>
        /// Checks if a user exists for the given email
        /// </summary>
        /// <param name="email">The email to use during lookup</param>
        /// <returns>The id of the user if found; A bad request result if the email is null or a NotFound result if the user wasn't found</returns>
        [Route("api/login")]
        [HttpGet]
        public IHttpActionResult CheckUserExistsForEmail(string email)
        {
            if (email == null)
            {
                return BadRequest();
            }
            AbstractUser abstractuser = db.AbstractUsers.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (abstractuser == null)
            {
                return NotFound();
            }
            return Ok(abstractuser);
        }

        [HttpGet]
        [Route("api/getUser")]
        public IHttpActionResult GetUserData(int id)
        {
            AbstractUser abstractuser = db.AbstractUsers.Find(id);
            if (abstractuser == null)
            {
                return BadRequest($"Could not fetch data for user with id {id}");
            }
            Entrepreneur entre = db.Entrepreneurs.Where(e => e.UserID == abstractuser.ID)
                .Include(e => e.User).Include(e => e.Establishment).FirstOrDefault();
            if (entre != null)
            {
                return Ok(entre);
            }
            User user = db.Users.Where(u => u.AbstractUserID == abstractuser.ID).Include(u => u.Subscriptions).FirstOrDefault();
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/register")]
        public IHttpActionResult RegisterUser([FromBody]string jsonArgs)
        {
            //Convert string to JSON object
            RegisterJSON body;
            try
            {
                body = JsonConvert.DeserializeObject<RegisterJSON>(jsonArgs, new RegisterJSONConverter());
            }
            catch (JsonSerializationException)
            {
                return BadRequest($"Could not parse the request. The body was: {jsonArgs}");
            }
            AbstractUser existingUser = db.AbstractUsers.Where(u => u.Email.Equals(body.Email)).FirstOrDefault();
            if (existingUser != null)
            {
                return BadRequest("User with email already exists");
            }
            //Add abstract user for email + firstname + lastname + salt + passwordHash
            AbstractUser newUser = new AbstractUser
            {
                Email = body.Email,
                FirstName = body.FirstName,
                LastName = body.LastName,
                Salt = body.Salt,
                Hash = body.Hash
            };

            if (body.IsEntrepreneur)
            {
                Entrepreneur entre = new Entrepreneur
                {
                    User = newUser
                };
                db.Entrepreneurs.Add(entre);
                db.SaveChanges();
                return Created("api/register", entre);
            }
            else {
                User user = new User
                {
                    AbstractUser = newUser,
                    Subscriptions = new List<Establishment>()
                };
                db.Users.Add(user);
                db.SaveChanges();
                return Created("api/register", user);
            }
        }

        // PUT: api/AbstractUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAbstractUser(int id, AbstractUser abstractUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != abstractUser.ID)
            {
                return BadRequest();
            }

            db.Entry(abstractUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbstractUserExists(id))
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

        // DELETE: api/AbstractUsers/5
        [ResponseType(typeof(AbstractUser))]
        public IHttpActionResult DeleteAbstractUser(int id)
        {
            AbstractUser abstractUser = db.AbstractUsers.Find(id);
            if (abstractUser == null)
            {
                return NotFound();
            }

            db.AbstractUsers.Remove(abstractUser);
            db.SaveChanges();

            return Ok(abstractUser);
        }

        #region Generated Methods
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AbstractUserExists(int id)
        {
            return db.AbstractUsers.Count(e => e.ID == id) > 0;
        }

        #endregion
    }

    public class RegisterJSON
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public bool IsEntrepreneur { get; set; }
    }

    class RegisterJSONConverter : JsonConverter<RegisterJSON>
    {
        public override RegisterJSON ReadJson(JsonReader reader, Type objectType, RegisterJSON existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            JObject jObject = JObject.Load(reader);
            if (jObject == null) return null;

            RegisterJSON target = new RegisterJSON();
            target.Email = jObject.Value<string>("Email");
            target.FirstName = jObject.Value<string>("FirstName");
            target.Hash = jObject.Value<string>("Hash");
            target.IsEntrepreneur = bool.Parse(jObject.Value<string>("IsEntrepreneur"));
            target.LastName = jObject.Value<string>("LastName");
            target.Salt = jObject.Value<string>("Salt");
            return target;
        }

        public override void WriteJson(JsonWriter writer, RegisterJSON value, JsonSerializer serializer)
        {
            throw new NotImplementedException("This converter shouldn't be used for writing to JSON");
        }
    }


}