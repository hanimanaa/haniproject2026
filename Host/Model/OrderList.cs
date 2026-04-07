using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Model
{
    [CollectionDataContract]
    public class OrderList : List<Order>
    {
        public OrderList() { }

        public OrderList(IEnumerable<Order> list) : base(list) { }

        
    }

}
