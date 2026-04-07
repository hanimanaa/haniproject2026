using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public int orderNum { get; set; }
        public DateTime orderDate { get; set; }
        public User user { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }
        public string orderStatus { get; set; }
    }
}
