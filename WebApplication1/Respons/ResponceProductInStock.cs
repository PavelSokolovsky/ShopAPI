using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Respons
{
    public partial class ResponceProductInStock
    {
        public ResponceProductInStock(ProductInStock inStock)
        {
            Product = inStock.Product;
            Min = inStock.amountMin;
            Max = inStock.amountMax;
            Current = inStock.amountCurrent;
            
        }
        public Product Product;

        public int Min ;
        public int Max ;
        public int Current ;
        
    }
}