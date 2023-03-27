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
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductInOrdersController : ApiController
    {
        private user12Entities db = new user12Entities();

        // GET: api/ProductInOrders
        public IQueryable<ProductInOrder> GetProductInOrder()
        {
            return db.ProductInOrder;
        }

        // GET: api/ProductInOrders/5
        [ResponseType(typeof(ProductInOrder))]
        public IHttpActionResult GetProductInOrder(int id)
        {
            ProductInOrder productInOrder = db.ProductInOrder.Find(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            return Ok(productInOrder);
        }

        // PUT: api/ProductInOrders/5
        [ResponseType(typeof(void))]
        //public IHttpActionResult PutProductInOrder()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != productInOrder.idProduct)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(productInOrder).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductInOrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/ProductInOrders
        [ResponseType(typeof(ProductInOrder))]
        public IHttpActionResult PostProductInOrder(int idOrder, string idProduct)
        {
           
            var content2 = db.Product.Where(y => y.barcode == idProduct).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductInOrder.Add(new ProductInOrder {idProduct = content2.id, idOrder = idOrder, amountProduct = 0});
            db.SaveChanges();
            return Ok();

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (ProductInOrderExists(.idProduct))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return CreatedAtRoute("DefaultApi", new { id = productInOrder.idProduct }, productInOrder);
        }

        // DELETE: api/ProductInOrders/5
        [ResponseType(typeof(ProductInOrder))]
        public IHttpActionResult DeleteProductInOrder(int id)
        {
            ProductInOrder productInOrder = db.ProductInOrder.Find(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            db.ProductInOrder.Remove(productInOrder);
            db.SaveChanges();

            return Ok(productInOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductInOrderExists(int id)
        {
            return db.ProductInOrder.Count(e => e.idProduct == id) > 0;
        }
    }
}