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
using WebApplication1.Respons;

namespace WebApplication1.Controllers
{
    public class ProductInStocksController : ApiController
    {
        private user12Entities db = new user12Entities();

        [ResponseType(typeof(ProductInStock))]
        public IHttpActionResult GetProductInStock(int id)
        {
            List<ProductInStock> order = db.ProductInStock.Where(i => i.Stock.idClient == id).ToList();
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order.ConvertAll<ResponceProductInStock>(i =>  new ResponceProductInStock(i)));
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

        [Route("api/ProductInStocks")]
        [ResponseType(typeof(ProductInStock))]
        [HttpPost]
        public IHttpActionResult PostProductInStock(string barcode, int idStock, int amountMin, int amountMax, int amountCurrent)
        {
            var content = db.Product.FirstOrDefault(i => i.barcode == barcode);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductInStock.Add(new ProductInStock { idProduct = content.id, idStock = idStock, amountMin = amountMin, amountMax = amountMax, amountCurrent = amountCurrent });
            db.SaveChanges();

            return Ok();
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