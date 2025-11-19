using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        public int productNum { get; set; }
        public string productName { get; set; }
        public double price { get; set; }
        public string imageUrl { get; set; }
        public string description { get; set; }
        public Category category { get; set; }
        public DateTime expiredDate { get; set; }
        public bool vegan { get; set; }

    }
}
