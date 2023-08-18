using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Inventory_Manager.Models;

namespace Inventory_Manager.Controllers
{
    public class AisleDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/aisledata/listaisle
        [HttpGet]
        [ResponseType(typeof(AisleDto))]
        public IHttpActionResult ListAisle()
        {
            var aislesDtos = db.Aisles.Select(a => new AisleDto
            {
                AisleId = a.AisleId,
                Name = a.Name,
                Desc = a.Desc,
                AisleCap = a.AisleCap,
            }).ToList();

            return Ok(aislesDtos);
        }

        // GET: api/aisleData/findaisle/3
        [HttpGet]
        [ResponseType(typeof(AisleDto))]
        public IHttpActionResult FindAisle(int id)
        {
            var aisle = db.Aisles.Find(id);
            if (aisle == null)
            {
                return NotFound();
            }

            var aisleDto = new AisleDto
            {
                AisleId = aisle.AisleId,
                Name = aisle.Name,
                Desc = aisle.Desc,
                AisleCap = aisle.AisleCap
            };

            return Ok(aisleDto);
        }

        // POST: api/aisledata/updateaisle/2
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateAisle(int id, Aisle aisle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aisle.AisleId)
            {
                return BadRequest();
            }

            db.Entry(aisle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AisleExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/aisleData/Addaisle
        [HttpPost]
        [ResponseType(typeof(Aisle))]
        public IHttpActionResult AddAisle(Aisle aisle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Aisles.Add(aisle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = aisle.AisleId }, aisle);
        }

        // POST: api/aisleData/Deleteaisle/2
        [HttpPost]
        [ResponseType(typeof(Aisle))]
        public IHttpActionResult DeleteAisle(int id)
        {
            var aisle = db.Aisles.Find(id);
            if (aisle == null)
            {
                return NotFound();
            }

            db.Aisles.Remove(aisle);
            db.SaveChanges();

            return Ok(aisle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AisleExists(int id)
        {
            return db.Aisles.Any(e => e.AisleId == id);
        }
    }
}
