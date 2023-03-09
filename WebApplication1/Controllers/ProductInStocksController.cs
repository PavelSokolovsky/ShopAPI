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
    public class ProductInStocksController : ApiController
    {
        private user12Entities db = new user12Entities();
        
        // GET: api/ProductInStocks
        public IQueryable<ProductInStock> GetProductInStock()
        {
            return db.ProductInStock;
        }
        [Route("api/inStock")]
        [ResponseType(typeof(Respons.ResponceProductInStock))]
        public IHttpActionResult GetInStock()
        {
            var goods = db.ProductInStock.ToList();
            if (goods == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
        

        // GET: api/ProductInStocks/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProductInStock(int id)
        {
            List<ProductInStock> productInStock = db.ProductInStock.Where(i => i.Stock.idClient == id).ToList();
            if (productInStock == null)
            {
                return NotFound();
            }

            return Ok(productInStock.ConvertAll(i => new Respons.ResponceProductInStock(i)));
        }

        // PUT: api/ProductInStocks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductInStock(int id, string barcode)
        {
            var client = db.ProductInStock.FirstOrDefault(i=> i.Stock.idClient == id && i.Product.barcode == barcode);
            client.amountCurrent = client.amountCurrent - 1;
            db.SaveChanges();
          
            

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductInStocks
        [ResponseType(typeof(ProductInStock))]
        public IHttpActionResult PostProductInStock(ProductInStock productInStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductInStock.Add(productInStock);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productInStock.id }, productInStock);
        }

        // DELETE: api/ProductInStocks/5
        [ResponseType(typeof(ProductInStock))]
        public IHttpActionResult DeleteProductInStock(int id)
        {
            ProductInStock productInStock = db.ProductInStock.Find(id);
            if (productInStock == null)
            {
                return NotFound();
            }

            db.ProductInStock.Remove(productInStock);
            db.SaveChanges();

            return Ok(productInStock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductInStockExists(int id)
        {
            return db.ProductInStock.Count(e => e.id == id) > 0;
        }
    }
}