using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Model
{
    [CollectionDataContract]
    public class CategoryList : List<Category>
    {
        public CategoryList() { }

        public CategoryList(IEnumerable<Category> list) : base(list) { }
    }
}
