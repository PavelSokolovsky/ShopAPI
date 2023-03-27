using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DefaultController : ApiController
    {
        private user12Entities db = new user12Entities();
        [HttpGet]
        public IHttpActionResult PostProductInStock(string barcode,int idStock, int amountMin, int amountMax, int amountCurrent)
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
    }
}
