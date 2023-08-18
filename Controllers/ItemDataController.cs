using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Inventory_Manager.Models;

namespace Inventory_Manager.Controllers
{
    public class ItemDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ItemData/ListItems
        [HttpGet]
        [ResponseType(typeof(ItemDto))]
        public IHttpActionResult ListItems()
        {
            List<ItemDto> itemDtos = db.Items.Select(item => new ItemDto
            {
                Id = item.Id,
                ItemName = item.ItemName,
                ItemType = item.ItemType,
                ItemCount = item.ItemCount,
                AisleId = item.AisleId,
                BBD = item.BBD,
            }).ToList();

            return Ok(itemDtos);
        }

        // GET: api/ItemData/FindItem/5
        [HttpGet]
        [ResponseType(typeof(ItemDto))]
        public IHttpActionResult FindItem(int id)
        {
            Item item = db.Items.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            ItemDto itemDto = new ItemDto
            {
                Id = item.Id,
                ItemName = item.ItemName,
                ItemType = item.ItemType,
                ItemCount = item.ItemCount,
                AisleId = item.AisleId,
                BBD = item.BBD,
            };

            return Ok(itemDto);
        }

        // POST: api/ItemData/UpdateItem/5
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/ItemData/AddItem
        [HttpPost]
        [ResponseType(typeof(Item))]
        public IHttpActionResult AddItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // POST: api/ItemData/DeleteItem/5
        [HttpPost]
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Any(e => e.Id == id);
        }
    }
}
