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
    public class PromotionsController : ApiController
    {
        private Swapps_Web_APIContext db = new Swapps_Web_APIContext();

        // GET: api/Promotions
        public IQueryable<Promotion> GetPromotions()
        {
            return db.Promotions
                .Include(pro => pro.Establishment)
                .Include(pro => pro.Establishment.Address)
                .Include(pro => pro.Establishment.Tags)
                .Include(pro => pro.Establishment.ServiceHours)
                .Include(pro => pro.Establishment.Events)
                .Include(pro => pro.Establishment.Promotions);
        }

        // GET: api/Promotions/5
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult GetPromotion(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            return Ok(promotion);
        }

        // PUT: api/Promotions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPromotion(int id, Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promotion.ID)
            {
                return BadRequest();
            }

            db.Entry(promotion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromotionExists(id))
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

        // POST: api/Promotions
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult PostPromotion(Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Promotions.Add(promotion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = promotion.ID }, promotion);
        }

        // DELETE: api/Promotions/5
        [ResponseType(typeof(Promotion))]
        public IHttpActionResult DeletePromotion(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return NotFound();
            }

            db.Promotions.Remove(promotion);
            db.SaveChanges();

            return Ok(promotion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PromotionExists(int id)
        {
            return db.Promotions.Count(e => e.ID == id) > 0;
        }
    }
}