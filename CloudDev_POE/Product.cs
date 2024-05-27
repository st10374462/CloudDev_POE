using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudDev_POE
{
    public class Product
    {
        public int productID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string imageSRC { get; set; }
    }
}