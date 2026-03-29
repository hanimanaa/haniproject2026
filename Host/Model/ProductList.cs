using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    [CollectionDataContract]
    public class ProductList : List<Product>
    {
        public ProductList() { }

        public ProductList(IEnumerable<Product> list) : base(list) { }
    }
}
